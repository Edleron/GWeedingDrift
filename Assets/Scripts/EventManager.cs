using System;

public class EventManager
{
        // SLIDER DETECT
        public static event Action<int> onDetectSlider;
        public static void Fire_onDetectSlider(int value) { onDetectSlider?.Invoke(value); }

        // PUAN DETECT
        public static event Action<int> onDetectPuan;
        public static void Fire_onDetectPuan(int value) { onDetectPuan?.Invoke(value); }

        // TIMER DETECT
        public static event Action<int> onDetectTimer;
        public static void Fire_onDetectTimer(int value) { onDetectTimer?.Invoke(value); }


        public static event Action onDetectRestart;
        public static void Fire_onDetectRestart() { onDetectRestart?.Invoke(); }
}
