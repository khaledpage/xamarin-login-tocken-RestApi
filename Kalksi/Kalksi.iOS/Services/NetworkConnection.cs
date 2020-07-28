using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreFoundation;
using Foundation;
using Kalksi.iOS.Services;
using Kalksi.Services;
using SystemConfiguration;
using UIKit;


[assembly: Xamarin.Forms.Dependency(typeof(NetworkConnection))]

namespace Kalksi.iOS.Services
{
    class NetworkConnection : INetworkConnection
    {
        //public bool IsConnected => throw new NotImplementedException();

        //public void CheckNetworkConnection()
        //{
        //    throw new NotImplementedException();
        //}


        //public bool InternetStatus()
        //{
        //    NetworkReachabilityFlags flags;
        //    bool defaultNetworkAvailable = IsNetwoekAvailable(out flags);
        //}

        //private event EventHandler Reachabilitychanged;
        //private void OnChange(NetworkReachabilityFlags flags)
        //{
        //    Reachabilitychanged?.Invoke(null, EventArgs.Empty);

        //}

        //private NetworkReachability defaultReachabilty;
        //public bool IsNetwoekAvailable(out NetworkReachabilityFlags flags)
        //{
        //    if (defaultReachabilty == null)
        //    {
        //        defaultReachabilty = new NetworkReachability(new System.Net.IPAddress(0));
        //        defaultReachabilty.SetNotification(OnChange);
        //        defaultReachabilty.Schedule(CFRunLoop.Current, CFRunLoop.ModeDefault);
        //    }
        //    if (!defaultReachabilty.TryGetFlags(out flags){
        //        return false;
        //    }
        //    return IsReachablewithoutRequirngConnection(NetworkReachabilityFlags flag);
        //}

        //private bool IsReachablewithoutRequirngConnection(NetworkReachabilityFlags networkReachabilityFlags, object flag)
        //{
        //    bool isreachable = (FlagsAttribute & NetworkReachabilityFlags.Reachable) != 0;
        //    bool noConnetionRequired = (FlagsAttribute & NetworkReachabilityFlags.ConnectionRequired == 0);

        //}
        public bool IsConnected { get; set; }

        public void CheckNetworkConnection()
        {

            NSString urlString = new NSString("https://captive.apple.com");

            NSUrl url = new NSUrl(urlString);

            NSUrlRequest request = new NSUrlRequest(url, NSUrlRequestCachePolicy.ReloadIgnoringCacheData, 3);

            NSData data = NSUrlConnection.SendSynchronousRequest(request, out NSUrlResponse response, out NSError error);

            NSString result = NSString.FromData(data, NSStringEncoding.UTF8);

            if (result.Contains(new NSString("Success")))
            {
                IsConnected = true;
            }

            else
            {
                IsConnected = false;
            }



        }
    }
}