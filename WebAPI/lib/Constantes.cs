using System;

namespace WebAPI.lib {
    static class Constantes {
        public const String urlSearchGoogle = "https://www.google.com/search?q=";
        public const String patternStart = "class=\"r\">";
        public const String patternEnd = "</a>";

        public const String patternStartURL = "?q=";
        public const String patternEndURL = "&";

        public static int lengthPatternStart = patternStart.Length;
        public static int lengthPatternEnd = patternEnd.Length;
        public static int lengthPatternStartURL = patternStartURL.Length;
        public static int lengthPatternEndURL = patternEndURL.Length;
    }
}
