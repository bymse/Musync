using System.Collections.Generic;
using Musync.Reader.Models.Vk;
using NUnit.Framework;

namespace Musync.Tests.Reader.Vk;

[TestFixture]
public class VkMusicPostTextParserTest
{
    [TestCaseSource(nameof(GetCases))]
    public MusicAlbum? Test(string text) => VkMusicPostTextParser.ParseToAlbumInfo(text);

    public static IEnumerable<TestCaseData> GetCases()
    {
        yield return new TestCaseData(
            @"AJ Suede & Televangel - Metatron's Cube (2022, Fake Four)

some text
more text
")
        {
            ExpectedResult = new MusicAlbum("AJ Suede & Televangel", "Metatron's Cube")
        };
        yield return new TestCaseData(@"Puritan Waste — You Can't Ignore The Enemy (2022, Unseen Force)
harsh | industrial | power electronics | electro | no wave | rhythmic noise | minimal

Новый кассетный дебют на известном американском лейбле, всегда")
        {
            ExpectedResult = new MusicAlbum("Puritan Waste", "You Can't Ignore The Enemy")
        };

        yield return new TestCaseData(@"Купи билеты
дорого")
        {
            ExpectedResult = null
        };

        yield return new TestCaseData(null)
        {
            ExpectedResult = null
        };

        yield return new TestCaseData("")
        {
            ExpectedResult = null
        };
    }
}