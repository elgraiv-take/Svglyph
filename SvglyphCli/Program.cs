/*
var obj=SvgIO.LoadSvg(@"E:\temp\Test.svg");
if(obj is null)
{
    return;
}
var icon = obj.ExtractIconPath(512.0f);
Console.Write(icon);

var dstSvg = new FontSvg();
dstSvg.AddIcon(icon);
dstSvg.Build();
SvgIO.SaveSvg(dstSvg,@"E:\temp\TestFont.svg");
*/

using Elgraiv.Svglyph;

var project = new SvglyphProject() { Glyphs = new(), };
project.Glyphs.Add(new() { SourceSvgPath = @"E:\temp\Test.svg" });

var processor = new SvglyphProcessor(project);

var outStream = new FileStream(@"E:\temp\TestFont.svg", FileMode.Create);
processor.Process(outStream);
