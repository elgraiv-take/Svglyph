using Svg;
using System.IO;
using System.Xml;

namespace Elgraiv.Svglyph;

public class SvgIO
{
    public static SvgFileObject? LoadSvg(string filePath)
    {
        try
        {
            using var stream = new FileStream(filePath, FileMode.Open);
            return LoadSvg(stream);
        }
        catch
        {
            return null;
        }

    }
    public static SvgFileObject? LoadSvg(Stream stream)
    {
        try
        {
            var doc = new XmlDocument();
            doc.Load(stream);

            var svg = SvgDocument.Open(doc);


            return new SvgFileObject(svg);
        }
        catch
        {
            return null;
        }

    }

    public static bool SaveSvg(FontSvg svg, string filePath)
    {

        try
        {
            using var stream = new FileStream(filePath, FileMode.Create);
            SaveSvg(svg, stream);

        }
        catch
        {
            return false;
        }
        return true;
    }

    public static bool SaveSvg(FontSvg svg, Stream stream)
    {
        try
        {
            svg.Document.Write(stream);

        }
        catch
        {
            return false;
        }
        return true;
    }
}
