using System.Text.Json;
using FluentAssertions;

namespace CSharpierRepro
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const int MaxValueOfMillisecondsDifference = 10000;
            var someObject = new SomeObject();
            string serializedEvent = JsonSerializer.Serialize(someObject);
            var deserializedEvent = JsonSerializer.Deserialize<SomeObject>(serializedEvent);
            // csharpier-ignore
            deserializedEvent.Should().BeEquivalentTo(someObject, options => options
                .Using<DateTime>(context =>
                    context.Subject.Should().BeCloseTo(context.Expectation, TimeSpan.FromMilliseconds(MaxValueOfMillisecondsDifference)))
                .WhenTypeIs<DateTime>()
            );
        }
    }

    public class SomeObject
    {
        public int Id { get; set; }
    }
}
