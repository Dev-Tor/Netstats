namespace Netstats.Network
{
    public interface IPageParserFactory
    {
        IPageParser GetParserFor(PageType type);
    }
}