namespace fiskaltrust.ifPOS.v1
{
    public interface IPOSFactory 
    {
        IPOS CreatePosAsync(POSOptions options);
    }
}
