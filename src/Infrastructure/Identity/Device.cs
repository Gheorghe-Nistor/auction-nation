using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cegeka.Auction.Infrastructure.Identity;

public class Device
{
    public static string GetDeviceType(string userAgent)
    {
        string message, deviceType = "Unknown";

        // Uncomment for testing
        //string userAgent = "mozilla / 5.0(windows nt 10.0; win64; x64) applewebkit / 537.36(khtml, like gecko) chrome / 112.0.0.0 safari / 537.36";                   // Windows
        //string userAgent = "Mozilla/5.0 (X11; Ubuntu; Linux x86_64; rv:88.0) Gecko/20100101 Firefox/88.0";                                                            // Linux
        //string userAgent = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/90.0.4430.93 Safari/537.36";                // Macbook
        //string userAgent = "Mozilla/5.0 (Linux; Android 11; SM-A125U) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.120 Mobile Safari/537.36";              // Samsung
        //string userAgent = "Mozilla/5.0 (iPhone; CPU iPhone OS 14_5 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/14.1 Mobile/15E148 Safari/604.1"; // iPhone
        //string userAgent = "Mozilla/5.0 (iPad; CPU OS 14_5 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/14.1 Mobile/15E148 Safari/604.1";          // iPad

        if (userAgent.Contains("mobile", StringComparison.OrdinalIgnoreCase))
        {
            if (userAgent.Contains("android", StringComparison.OrdinalIgnoreCase))
            {
                deviceType = "Android";
            }
            else if (userAgent.Contains("iphone", StringComparison.OrdinalIgnoreCase))
            {
                deviceType = "iPhone";
            }
            else if (userAgent.Contains("ipad", StringComparison.OrdinalIgnoreCase))
            {
                deviceType = "iPad";
            }
        }
        else
        {
            if (userAgent.Contains("windows", StringComparison.OrdinalIgnoreCase))
            {
                deviceType = "Windows";
            }
            else if (userAgent.Contains("linux", StringComparison.OrdinalIgnoreCase))
            {
                deviceType = "Linux";
            }
            else if (userAgent.Contains("mac", StringComparison.OrdinalIgnoreCase))
            {
                deviceType = "Mac";
            }
        }

        message = $"{deviceType}";

        return message;
    }
}
