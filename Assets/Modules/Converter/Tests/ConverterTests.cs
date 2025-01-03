using NUnit.Framework;

namespace Modules.Converter
{
    public sealed class ConverterTests
    {
        //TODO: Написать конвертер ресурсов по TDD
        
        [TestCase(5, 10)]
        [TestCase(3, 2)]
        [TestCase(1, 100)]
        [TestCase(255, 1)]
        public void Instantiate(int width, int height)
        {
          
        }

    }
}