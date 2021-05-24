using System;
using System.Linq;
using System.Threading.Tasks;

namespace InteropCodeGenerator
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            string[] headers = new string[]
            {
                "achievements_c",
                "achievements_manager_c",
                "errors_c",
                "events_c",
                "http_call_c",
                "leaderboard_c",
                "matchmaking_c",
                "multiplayer_activity_c",
                "multiplayer_c",
                "multiplayer_manager_c",
                "notification_c",
                "presence_c",
                "privacy_c",
                "profile_c",
                "real_time_activity_c",
                "social_c",
                "social_manager_c",
                "string_verify_c",
                "title_managed_statistics_c",
                "title_storage_c",
                "xbox_live_context_c",
            };

            foreach (string header in headers)
            {
                await GenerateXsapiArgs(header);
            }
        }

        static async Task GenerateXsapiArgs(string header)
        {
            string file = @"D:\sdk.xsapi.internal\Include\xsapi-c\services_c.h";
            string traverse = @"D:\sdk.xsapi.internal\Include\xsapi-c\" + header + ".h";
            string ns = "XGR.Interop";
            string outputFile = @"D:\sdk.xsapi.internal\Include\xsapi-c\interop\" + header;
            string[] includeDirs = new string[]
            {
                @"D:\sdk.xsapi.internal\Include",
                @"D:\sdk.xsapi.internal\External\xal\Source\Xal\Include",
                @"D:\sdk.xsapi.internal\External\xal\External\libHttpClient\Include"
            };
            string[] additional = new string[]
            {
                "-Wno-deprecated-declarations"
            };
            string[] config = new string[]
            {
                "multi-file"
            };

            string cmdArgs = "";
            cmdArgs += "-f " + file;
            cmdArgs += " --traverse " + traverse;
            cmdArgs += " -n " + ns;
            cmdArgs += " -o " + outputFile;
            cmdArgs += " --config " + string.Join(' ', config);
            cmdArgs += " --include-directory " + string.Join(' ', includeDirs);
            cmdArgs += " --additional " + string.Join(' ', additional);

            await ClangSharp.Program.Main(cmdArgs.Split(' '));
        }
    }
}
