using Svg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elgraiv.Svglyph;

public class SvgIcon
{
    private SvgPath _path;
    internal SvgPath PathData => _path;
    internal SvgIcon()
    {
        _path = new SvgPath();
    }

    internal SvgIcon(SvgPath path)
    {
        _path = path;
    }
}
