using Svg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Unicode;
using System.Threading.Tasks;
using System.Xml;

namespace Elgraiv.Svglyph;

public class FontSvg
{

    private List<SvgIcon> _icons = new();
    private SvgDocument _document = new();

    internal SvgDocument Document => _document;
    public void AddIcon(SvgIcon icon)
    {
        _icons.Add(icon);
    }

    public void Build(float height)
    {
        _document.Children.Clear();
        var defs = new SvgDefinitionList();
        _document.Width = new SvgUnit(SvgUnitType.Percentage, 100.0f);
        _document.Height = new SvgUnit(SvgUnitType.Percentage, 100.0f);
        _document.Children.Add(defs);
        _document.ViewBox = new SvgViewBox(0.0f, 0.0f, height, height);

        var fontTag = new SvgFont();
        fontTag.HorizAdvX = height;
        fontTag.VertAdvY = height;
        var fontFace = new SvgFontFace();
        fontFace.Ascent = height;
        fontFace.Descent = 0;
        fontFace.UnitsPerEm = height;
        fontFace.FontFamily = "Test";

        fontTag.Children.Add(fontFace);

        defs.Children.Add(fontTag);

        foreach (var icon in _icons)
        {
            var glyph = new SvgGlyph();
            glyph.GlyphName = "a";
            glyph.Unicode = "a";
            glyph.PathData = icon.PathData.PathData;
            fontTag.Children.Add(glyph);
        }

    }
}
