namespace Netstats.Network
{
    public interface IPageDescriptor
    {
        bool IsMatch(IPage page);
    }
}