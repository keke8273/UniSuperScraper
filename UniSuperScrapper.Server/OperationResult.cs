using System.Runtime.Serialization;

namespace UpSoft.UniSuperScrapper.ClientService
{
    [DataContract]
    public enum OperationResult
    {
        [EnumMember]
        Successful,

        [EnumMember]
        Fialed,
    }
}