using Spring.Aop;
using System;

namespace Proasoft.Aspectos
{
    public class ThrowsException : IThrowsAdvice
    {
        public void AfterThrowing(Exception ex)
        {
            throw ex;
        }
    }
}
