using System.Security.Cryptography;

namespace FileHashGeneratorAndValidator.Helpers
{
    public static class AlgorithmsHelper
    {
        public static int GetAlgorithmHashLength(Enums.HashAlgorithm selectedAlgorith)
        {
            return selectedAlgorith switch
            {
                Enums.HashAlgorithm.MD5 => 32,
                Enums.HashAlgorithm.SHA1 => 40,
                Enums.HashAlgorithm.SHA256 or Enums.HashAlgorithm.SHA3_256 => 64,
                Enums.HashAlgorithm.SHA384 or Enums.HashAlgorithm.SHA3_384 => 96,
                Enums.HashAlgorithm.SHA512 or Enums.HashAlgorithm.SHA3_512 => 128,
                _ => 0
            };
        }

        public static Enums.HashAlgorithm[] GetAlgorithmsByLength(int length)
        {
            return length switch
            {
                32 => [Enums.HashAlgorithm.MD5],
                40 => [Enums.HashAlgorithm.SHA1],
                64 => [Enums.HashAlgorithm.SHA256, Enums.HashAlgorithm.SHA3_256],
                96 => [Enums.HashAlgorithm.SHA384, Enums.HashAlgorithm.SHA3_384],
                128 => [Enums.HashAlgorithm.SHA512, Enums.HashAlgorithm.SHA3_512],
                _ => []
            };
        }

        public static HashAlgorithmName GetAlgorithmName(Enums.HashAlgorithm algorithm)
        {
            return algorithm switch
            {
                Enums.HashAlgorithm.MD5 => HashAlgorithmName.MD5,
                Enums.HashAlgorithm.SHA1 => HashAlgorithmName.SHA1,
                Enums.HashAlgorithm.SHA256 => HashAlgorithmName.SHA256,
                Enums.HashAlgorithm.SHA384 => HashAlgorithmName.SHA384,
                Enums.HashAlgorithm.SHA512 => HashAlgorithmName.SHA512,
                Enums.HashAlgorithm.SHA3_256 => HashAlgorithmName.SHA3_256,
                Enums.HashAlgorithm.SHA3_384 => HashAlgorithmName.SHA3_384,
                Enums.HashAlgorithm.SHA3_512 => HashAlgorithmName.SHA3_512,
                _ => throw new Exception("Algoritms not suported")
            };
        }
    }
}
