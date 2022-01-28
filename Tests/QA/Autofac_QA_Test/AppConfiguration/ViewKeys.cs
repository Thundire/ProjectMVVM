using System;

namespace Autofac_QA_Test.AppConfiguration
{
    public static class ViewsKeys
    {
        public static readonly string Main = Guid.NewGuid().ToString();
        public static readonly string Confirm = Guid.NewGuid().ToString();
        public static readonly string NumbersEditor = Guid.NewGuid().ToString();
        public static readonly string Navigation = Guid.NewGuid().ToString();
    }
}