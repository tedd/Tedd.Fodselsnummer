using System;

using Xunit;

namespace Tedd.Fodselsnummer.Test
{
    public class FodselsnummerTests
    {
        [Fact]
        public void FodselsnummerTest1()
        {
            var nr = new[] { "19121999768", "19121999334", "19121999172", "19121998974", "19121998702", "19121998540", "19121998389", "19121998117", "19121997919", "19121997757", "19121997595", "19121997323", "19121997161", "19121996963", "19121996378", "19121996106", "19121995908", "19121995746", "19121995584", "19121995312", "19121995150", "19121994952", "19121994790", "19121994529", "19121994367", "19121993735", "19121993573", "19121993301", "19121992941", "19121992518", "19121992356", "19121992194", "19121991996", "19121991724", "19121991562", "19121991139", "19121990930", "19121990779", "19121990507", "19121990345", "19121990183", "19121989967", "19121989533", "19121989371", "19121988901", "19121988588", "19121988316", "19121988154", "19121987956", "19121987794", "19121987522", "19121987360", "19121986739", "19121986577", "19121986305", "19121986143", "19121985945", "19121985783", "19121985511", "19121985198", "19121984728", "19121984566", "19121984132", "19121983934", "19121983772", "19121983500", "19121983349", "19121983187", "19121982989", "19121982717", "19121982555", "19121982393", "19121982121", "19121981923", "19121981761", "19121981338", "19121981176", "19121980978", "19121980706", "19121980544", "19121980382", "19121980110", "19121979732", "19121979570", "19121979309", "19121979147", "19121978949", "19121978787", "19121978515", "19121978353", "19121978191", "19121977993", "19121977721", "19121977136", "19121976938", "19121976776", "19121976504", "19121976342", "19121976180", "19121975982", "19121975710", "19121975559", "19121975397", "19121975125", "19121974927", "19121974765", "19121974331", "19121973971", "19121973548", "19121973386", "19121973114", "19121972916", "19121972754", "19121972592", "19121972320", "19121972169", "19121971960", "19121971537", "19121971375", "19121971103", "19121970905", "19121970743", "19121970581", "19121970158", "19121969931", "19121969508", "19121969346", "19121969184", "19121968986", "19121968714", "19121968552", "19121968390", "19121968129", "19121967920", "19121967769", "19121967335", "19121967173", "19121966975", "19121966703", "19121966541", "19121966118", "19121965758", "19121965596", "19121965324", "19121965162", "19121964964", "19121964530", "19121964379", "19121964107", "19121963909", "19121963747", "19121963585", "19121963313", "19121963151", "19121962953", "19121962791", "19121962368", "19121961736", "19121961574", "19121961302", "19121961140", "19121960942", "19121960780", "19121960519", "19121960357", "19121960195", "19121959979", "19121959707", "19121959545", "19121959383", "19121959111", "19121958913", "19121958751", "19121958328", "19121958166", "19121957968", "19121957534", "19121957372", "19121957100", "19121956902", "19121956740", "19121956589", "19121956317", "19121956155", "19121955957", "19121955795", "19121955523", "19121955361", "19121954578", "19121954306", "19121954144", "19121953946", "19121953784", "19121953512", "19121953350", "19121953199", "19121952990", "19121952729", "19121952567", "19121952133", "19121951935", "19121951773", "19121951501", "19121951188", "19121950718", "19121950556", "19121950394", "19121950122", "19121999849", "19121999687", "19121999415", "19121999253", "19121999091", "19121998893", "19121998621", "19121998036", "19121997838", "19121997676", "19121997404", "19121997242", "19121997080", "19121996882", "19121996610", "19121996459", "19121996297", "19121996025", "19121995827", "19121995665", "19121995231", "19121994871", "19121994448", "19121994286", "19121994014", "19121993816", "19121993654", "19121993492", "19121993220", "19121993069", "19121992860", "19121992437", "19121992275", "19121992003", "19121991805", "19121991643", "19121991481", "19121991058", "19121990698", "19121990426", "19121990264", "19121989886", "19121989614", "19121989452", "19121989290", "19121989029", "19121988820", "19121988669", "19121988235", "19121988073", "19121987875", "19121987603", "19121987441", "19121987018", "19121986658", "19121986496", "19121986224", "19121986062", "19121985864", "19121985430", "19121985279", "19121985007", "19121984809", "19121984647", "19121984485", "19121984213", "19121984051", "19121983853", "19121983691", "19121983268", "19121982636", "19121982474", "19121982202", "19121982040", "19121981842", "19121981680", "19121981419", "19121981257", "19121981095", "19121980897", "19121980625", "19121980463", "19121979813", "19121979651", "19121979228", "19121979066", "19121978868", "19121978434", "19121978272", "19121978000", "19121977802", "19121977640", "19121977489", "19121977217", "19121977055", "19121976857", "19121976695", "19121976423", "19121976261", "19121975478", "19121975206", "19121975044", "19121974846", "19121974684", "19121974412", "19121974250", "19121974099", "19121973890", "19121973629", "19121973467", "19121973033", "19121972835", "19121972673", "19121972401", "19121972088", "19121971618", "19121971456", "19121971294", "19121971022", "19121970824", "19121970662", "19121970239", "19121970077", "19121969850", "19121969699", "19121969427", "19121969265", "19121968633", "19121968471", "19121968048", "19121967688", "19121967416", "19121967254", "19121967092", "19121966894", "19121966622", "19121966460", "19121966037", "19121965839", "19121965677", "19121965405", "19121965243", "19121965081", "19121964883", "19121964611", "19121964298", "19121964026", "19121963828", "19121963666", "19121963232", "19121963070", "19121962872", "19121962600", "19121962449", "19121962287", "19121962015", "19121961817", "19121961655", "19121961493", "19121961221", "19121960861", "19121960438", "19121960276", "19121960004", "19121959898", "19121959626", "19121959464", "19121959030", "19121958832", "19121958670", "19121958409", "19121958247", "19121958085", "19121957887", "19121957615", "19121957453", "19121957291", "19121956821", "19121956236", "19121956074", "19121955876", "19121955604", "19121955442", "19121955280", "19121955019", "19121954810", "19121954659", "19121954497", "19121954225", "19121954063", "19121953865", "19121953431", "19121953008", "19121952648", "19121952486", "19121952214", "19121952052", "19121951854", "19121951692", "19121951420", "19121951269", "19121950637", "19121950475", "19121950203", "19121950041" };
            for (var i = 0; i < nr.Length; i++)
            {
                var pn = nr[i];
                var result = FodselsnummerValidator.Validate(pn);
                if (!result.Success)
                {
                    Console.WriteLine(result.ErrorMessage);
                }
                else
                {
                    Console.WriteLine($"F�dselsnummer type: {result.Type}");
                    Console.WriteLine($"Kj�nn: {result.Gender}");
                    Console.WriteLine($"F�dselsdato: {result.Birthday}");
                }
                Assert.True(result.Success, $"{i}: {pn}: [{result.ErrorCode}] {result.ErrorMessage}");
            }
        }
    }
}