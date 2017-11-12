using AngleSharp.Dom.Html;

namespace Netstats.Network
{
    public interface IPageParser
    {
        PageType For { get; }

        string Parse(IHtmlDocument document);
    }
}