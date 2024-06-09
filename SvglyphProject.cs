using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Elgraiv.Svglyph;

public class SvglyphProject
{
    public string FontName { get; set; } = "NewFont";
    public float Size { get; set; } = 1024.0f;

    public required List<GlyphRecord> Glyphs { get; init; }
}

public class GlyphRecord
{
    public string CharCode { get; set; } = new("\ue000");
    public string GlyphName { get; set; } = "NewGlyph";

    public string SourceSvgPath { get; set; } = string.Empty;
}
