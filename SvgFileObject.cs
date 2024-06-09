using Svg;
using Svg.Pathing;
using Svg.Transforms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elgraiv.Svglyph;

public class SvgFileObject
{
    private SvgDocument _document;
    internal SvgFileObject(SvgDocument document)
    {
        _document = document;
    }

    public bool Validate()
    {
        var valid = true;
        valid = valid && _document.ViewBox.Height == _document.ViewBox.Width;
        return valid;
    }

    public SvgIcon ExtractIconPath(float height)
    {
        var path = FindPath(_document);
        if(path is null)
        {
            return new SvgIcon();
        }
        var transform = path.ParentsAndSelf.SelectMany(elem => elem.Transforms ?? Enumerable.Empty<SvgTransform>()).OfType<SvgTransform>().ToArray();

        var render = SvgRenderer.FromNull();
        var originHeight = _document.ViewBox.Height;

        var scale = height / originHeight;

        var total = transform.Aggregate(new Matrix(), (accum, transform) => { accum.Multiply(transform.Matrix, MatrixOrder.Append); return accum; });

        //render.Transform = total;
        var seg = path.Path(render);


        var vInverse = new Matrix(scale, 0.0f, 0.0f, -scale, 0.0f, height);
        
        total.Multiply(vInverse, MatrixOrder.Append);

        var converted = new SvgPath();
        var svgSegments= new SvgPathSegmentList();
        converted.PathData = svgSegments;
        var bezier = new List<PointF>();
        var points = (PointF[])seg.PathPoints.Clone();
        total.TransformPoints(points);
        foreach (var (pointOrigin,point, t) in seg.PathPoints.Zip(points,seg.PathTypes))
        {
            var type = (PathPointType)(~((byte)PathPointType.CloseSubpath) & t);
            Console.WriteLine($"{type}:{point}  - {pointOrigin}");
            var close = false;
            if (((byte)PathPointType.CloseSubpath & t) != 0)
            {
                Console.WriteLine($"{PathPointType.CloseSubpath}");
                close = true;
            }
            switch (type)
            {
                case PathPointType.Start:
                    svgSegments.Add(new SvgMoveToSegment(false, point));
                    break;
                case PathPointType.Line:
                    svgSegments.Add(new SvgLineSegment(false, point));
                    break;
                case PathPointType.Bezier:
                    bezier.Add(point);
                    if (bezier.Count >= 3)
                    {
                        svgSegments.Add(new SvgCubicCurveSegment(false, bezier[0], bezier[1], bezier[2]));
                        bezier.Clear();
                    }
                    break;
                //case PathPointType.Bezier3:
                    //break;
                case PathPointType.PathTypeMask:
                    break;
                case PathPointType.DashMode:
                    break;
                case PathPointType.PathMarker:
                    break;
                case PathPointType.CloseSubpath:
                    break;
                default:
                    break;
            }
            if (close)
            {
                svgSegments.Add(new SvgClosePathSegment(false));
            }
        }
        converted.InvalidateChildPaths();

        return new SvgIcon(converted);
        
    }

    private SvgPath? FindPath(SvgElement element)
    {
        if (!element.HasChildren())
        {
            return null;
        }
        foreach(var child in element.Children)
        {
            if(child is SvgPath path)
            {
                return path;
            }
            var pathInChild = FindPath(child);
            if(pathInChild is not null)
            {
                return pathInChild;
            }
        }
        return null;
    }
}
