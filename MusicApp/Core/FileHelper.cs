using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Core
{
    public static class FileHelper
    {
        public enum ImageExtension
        {
            JPG = 0,
            PNG,
            Unknown
        }

        public static ImageExtension GetImageExtension(string path)
        {
            int lastDotIndex = 0;
            for(int i = path.Length-1; i > 0; i--)
            {
                if(path[i] == '.')
                {
                    lastDotIndex = i;
                    break;
                }
            }
            var span = path.ToLower().AsSpan(lastDotIndex+1, (path.Length - lastDotIndex)-1);

            switch (span.Length)
            {
                case 3:
                    {
                        switch (((ulong)span[0] << 16 | span[1]) << 16 | span[2])
                        {
                            case ((ulong)'j' << 16 | 'p') << 16 | 'g':
                                {
                                    return ImageExtension.JPG;
                                }
                            case ((ulong)'p' << 16 | 'n') << 16 | 'g':
                                {
                                    return ImageExtension.PNG;
                                }
                            default:
                                return ImageExtension.Unknown;
                        }
                    }
                default:
                    {
                        return ImageExtension.Unknown;
                    }
            }

            
        }
    }
}
