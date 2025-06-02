namespace fiskaltrust.ifPOS.v2.Cases;
public enum State : long
{
    Success = 0x0,
    Error = 0xEEEE_EEEE,
    Fail = 0xFFFF_FFFF
}
