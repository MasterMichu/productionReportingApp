using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Production_reporting_app.Models
{
    public static class DelegateContainer
    {
        
        public delegate void ClosePopupSendData(DashboardData data);
        public delegate void ShowLoadingScreen();
        public delegate void CloseLoadingScreen();
        public delegate void ClosePopupDeleteData(DashboardData data);
        public static event CloseLoadingScreen OrderCloseLoadingScreen;
        public static event ShowLoadingScreen OrderShowLoadingScreen;
        public static event ClosePopupSendData OnClosePopupSendData;
        public static event ClosePopupDeleteData OnClosePopupDeleteData;


        public static void RaiseOnClosePopupSendData(DashboardData data)
        {
            OnClosePopupSendData?.Invoke(data);


        }
        public static void RaiseOnClosePopupDeleteData(DashboardData data)
        {
            OnClosePopupDeleteData?.Invoke(data);


        }
        public static void RaiseOrderShowLoadingScreen()
        {
            OrderShowLoadingScreen?.Invoke();


        }
        public static void RaiseOrderCloseLoadingScreen()
        {
            OrderCloseLoadingScreen?.Invoke();


        }

    }
}
