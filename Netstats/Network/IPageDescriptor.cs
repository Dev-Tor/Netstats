using AngleSharp.Dom.Html;

namespace Netstats.Network
{
    public interface IPageDescriptor
    {
        PageType For { get; }

        bool IsMatch(IHtmlDocument pageContent);
    }
}