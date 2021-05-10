using BenchmarkDotNet.Attributes;
using ConsoleApp.Benchmark.Base;

namespace ConsoleApp.Benchmark
{
    public class BenchmarkNumberService : BaseBenchmarkService
    {
        #region NoString

        //[Benchmark]
        //public void Empty_NoString()
        //{
        //    CalcNoString(Empty);
        //}

        //[Benchmark]
        //public void NumberOnly_NoString()
        //{
        //    CalcNoString(NumberOnly);
        //}

        //[Benchmark]
        //public void Formula1_NoString()
        //{
        //    CalcNoString(Formula1);
        //}

        //[Benchmark]
        //public void Formula2_NoString()
        //{
        //    CalcNoString(Formula2);
        //}

        //[Benchmark]
        //public void Formula3_NoString()
        //{
        //    CalcNoString(Formula3);
        //}

        [Benchmark]
        public void Formula4_NoString()
        {
            CalcNoString(Formula4);
        }

        //[Benchmark]
        //public void Formula5_NoString()
        //{
        //    CalcNoString(Formula5, Arg1, Arg2, Arg3, Arg4);
        //}

        //[Benchmark]
        //public void Formula6_NoString()
        //{
        //    CalcNoString(Formula6, Arg1, Arg2, Arg3, Arg4, Arg5, Arg6, Arg7, Arg8, Arg9, Arg10);
        //}

        //[Benchmark]
        //public void Formula7_NoString()
        //{
        //    CalcNoString(Formula7);
        //}

        //[Benchmark]
        //public void Formula8_NoString()
        //{
        //    CalcNoString(Formula8);
        //}

        //[Benchmark]
        //public void Formula9_NoString()
        //{
        //    CalcNoString(Formula9, Arg1);
        //}

        //[Benchmark]
        //public void Formula10_NoString()
        //{
        //    CalcNoString(Formula10);
        //}

        #endregion

        //#region MxParser

        //[Benchmark]
        //public void Empty_MxParser()
        //{
        //    CalcMxParser(Empty);
        //}

        //[Benchmark]
        //public void NumberOnly_MxParser()
        //{
        //    CalcMxParser(NumberOnly);
        //}

        //[Benchmark]
        //public void Formula1_MxParser()
        //{
        //    CalcMxParser(Formula1);
        //}

        //[Benchmark]
        //public void Formula2_MxParser()
        //{
        //    CalcMxParser(Formula2);
        //}

        //[Benchmark]
        //public void Formula3_MxParser()
        //{
        //    CalcMxParser(Formula3);
        //}

        //[Benchmark]
        //public void Formula4_MxParser()
        //{
        //    CalcMxParser(Formula4);
        //}

        //[Benchmark]
        //public void Formula5_MxParser()
        //{
        //    CalcMxParser(Formula5, Arg1, Arg2, Arg3, Arg4);
        //}

        //[Benchmark]
        //public void Formula6_MxParser()
        //{
        //    CalcMxParser(Formula6, Arg1, Arg2, Arg3, Arg4, Arg5, Arg6, Arg7, Arg8, Arg9, Arg10);
        //}

        //[Benchmark]
        //public void Formula7_MxParser()
        //{
        //    CalcMxParser(Formula7);
        //}

        //[Benchmark]
        //public void Formula8_MxParser()
        //{
        //    CalcMxParser(Formula8);
        //}

        //[Benchmark]
        //public void Formula9_MxParser()
        //{
        //    CalcMxParser(Formula9, Arg1);
        //}

        //[Benchmark]
        //public void Formula10_MxParser()
        //{
        //    CalcMxParser(Formula10);
        //}

        //#endregion

    }
}
