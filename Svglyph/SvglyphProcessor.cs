namespace Elgraiv.Svglyph;

public class SvglyphProcessor
{
    private SvglyphProject _data;

    public SvglyphProcessor(SvglyphProject data)
    {
        _data = data;
    }

    public void Process(Stream stream)
    {
        var font = new FontSvg();
        foreach (var file in _data.Glyphs)
        {
            var src = SvgIO.LoadSvg(file.SourceSvgPath);
            if (src is null)
            {
                //エラー
                continue;
            }
            var icon = src.ExtractIconPath(_data.Size);
            if(icon is null)
            {
                //エラー
                continue;
            }
            font.AddIcon(icon);
        }

        font.Build(_data.Size);

        SvgIO.SaveSvg(font, stream);
    }
}