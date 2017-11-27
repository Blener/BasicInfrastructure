
using BasicInfrastructureExtensions.Extensions;

namespace BasicInfrastructurePersistence.Tests.TestEntities
{
    public enum TestEnum
    {

        [LocalizedDescription("Number One")]
        FirstItem,
        SecondItem,
        Third_Item,
        [LocalizedDescription("Fourth-Item")]
        FourthItem,
        [LocalizedDescription("Fifth_Item")]
        FifthItem
    }
}
