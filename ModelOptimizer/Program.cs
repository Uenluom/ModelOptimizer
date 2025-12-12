using System.Diagnostics;

static void CopyDirectory(string sourceDir, string destinationDir, bool recursive)
{
    // Get information about the source directory
    var dir = new DirectoryInfo(sourceDir);

    // Check if the source directory exists
    if (!dir.Exists)
        throw new DirectoryNotFoundException($"Source directory not found: {dir.FullName}");

    // Cache directories before we start copying
    DirectoryInfo[] dirs = dir.GetDirectories();

    // Create the destination directory
    Directory.CreateDirectory(destinationDir);

    // Get the files in the source directory and copy to the destination directory
    foreach (FileInfo file in dir.GetFiles())
    {
        string targetFilePath = Path.Combine(destinationDir, file.Name);
        try
        {
            file.CopyTo(targetFilePath);
        }
        catch
        {

        }
    }

    // If recursive and copying subdirectories, recursively call this method
    if (recursive)
    {
        foreach (DirectoryInfo subDir in dirs)
        {
            string newDestinationDir = Path.Combine(destinationDir, subDir.Name);
            CopyDirectory(subDir.FullName, newDestinationDir, true);
        }
    }
}

void Write(string text = "", ConsoleColor foreColor = ConsoleColor.White, ConsoleColor backColor = ConsoleColor.Black)
{
    Console.ForegroundColor = foreColor;
    Console.BackgroundColor = backColor;
    Console.Write(text);
}
void WriteLine(string text = "", ConsoleColor foreColor = ConsoleColor.White, ConsoleColor backColor = ConsoleColor.Black)
{
    Console.ForegroundColor = foreColor;
    Console.BackgroundColor = backColor;
    Console.WriteLine(text);
}

const float MiB = 1024 * 1024.0f;

Console.OutputEncoding = System.Text.Encoding.UTF8;

WriteLine("\x1b[5mModelOptimizer v1.0\x1b[25m");
Write("  by ");
WriteLine("Uenluom", ConsoleColor.Red);

WriteLine();
WriteLine("This program uses OptiPNG, a project by:");
WriteLine();

string[] optiPngAuthors = [
    "Adam Ciarcinski", "Brian McQuade", "Elias Pipping", "Fabien Barbier", "Friedrich Preuss", 
    "Maciej Pilichowski", "Matthew Fearnley", "Nelson A. de Oliveira", "Niels de Koning", 
    "Oliver Schneider", "Petr Gajdos", "Piotr Bandurski", "Priit Laes", "Ramona C. Truta", 
    "Sebastian Pipping", "Stefan Brüns", "Thomas Hurst", "Till Maas", "Ville Skyttä", 
    "Vincent Lefèvre", "Yuen Ho Wong"
];
int counter = 0;

for (int i = 0; i < optiPngAuthors.Length  ; i += 4)
{
    string author1 = i < optiPngAuthors.Length ? optiPngAuthors[i] : "";
    string author2 = i + 1 < optiPngAuthors.Length ? optiPngAuthors[i+ 1] : "";
    string author3 = i + 2 < optiPngAuthors.Length ? optiPngAuthors[i+ 2] : "";
    string author4 = i + 3 < optiPngAuthors.Length ? optiPngAuthors[i+ 3] : "";

    WriteLine(string.Format("{0,24}{1,24}{2,24}{3,24}", author1, author2, author3, author4), ConsoleColor.Yellow);
}

WriteLine();
WriteLine("OptiPNG is released under the ZLIB license, which can be found here:", ConsoleColor.DarkGray);
WriteLine("https://optipng.sourceforge.net/license.txt", ConsoleColor.DarkGray);

WriteLine();


//=====================================================================================================================
//
// Find the directory
//
//=====================================================================================================================


string choice = "";
string directoryPath = "";
int easterEgg = 0;

//              ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣀⣠⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
//              ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣤⣾⣿⢿⣟⠶⠦⣄⣀⠀⠀⠀⠀⠀⠀⠀⣀⡤⠖⠛⠉⣰⠏⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
//              ⠀⠀⠀⠀⢸⣷⣦⢤⣄⣀⠀⠀⠀⠀⠀⠀⠀⠀⣠⡾⣫⣾⣿⠃⠀⠘⠷⣆⠀⠙⣷⣆⠀⠀⢀⣠⠞⠉⣀⠄⠀⠀⣿⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
//              ⠀⠀⠀⠀⢸⠟⠀⠀⠀⠈⠙⠳⠦⣄⠀⠀⢠⡾⢋⣴⠋⣾⡇⠀⠀⠀⠀⢙⣷⣿⠋⠸⣇⣠⠟⣡⠴⠛⠒⡻⠿⠒⣿⢿⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
//              ⠀⠀⠀⠀⡾⠀⠀⠘⢛⣳⡦⣄⠀⠈⠙⢦⣿⠛⣿⣧⣄⣿⣀⣤⣴⡾⠿⠛⠛⢿⡄⢠⡟⠁⠀⠉⠓⠦⣄⡭⠷⠂⠀⢸⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
//              ⠀⠀⠀⢠⡇⠀⠀⠴⢯⣀⠀⠈⠳⡄⠀⠀⠹⣇⢹⣌⠙⣿⠉⠀⠀⠀⠀⠀⠀⠘⣷⣾⠉⠲⣄⠀⠀⠀⠀⠉⠲⢄⣠⡏⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
//              ⠀⠀⠀⢸⠁⠀⠀⣠⣞⣍⣤⠀⠀⠈⠀⠀⠀⠉⠻⠿⣅⣻⣇⠀⠀⣀⣀⣤⡶⠾⠋⠁⠀⠀⠈⠳⠀⠀⠀⠀⠀⠀⠙⠲⣀⠀⠀⠀⠀⠀⠀⠀⠀⠀
//              ⠀⠀⠀⢸⠀⠀⠈⢉⡶⠋⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢦⣉⠉⠛⠉⠉⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⠀⠀⠀⠀⠀⠀⠈⠳⡀⠀⠀⠀⠀⠀⠀⠀
//              ⠀⠀⠀⠸⡇⠀⢠⠟⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⠀⢳⡈⣏⢧⡀⠀⠀⢠⠀⠀⠀⠀⠀⠘⢦⠀⠀⠀⠘⢇⠀⠀⠀⠀⠀⠀⠀⠙⣆⠀⠀⠀⠀⠀⠀
//              ⠀⠀⠀⠀⢷⣠⠋⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣼⠀⢸⣇⢸⡄⠱⣄⠀⠀⢧⠀⠀⠀⠀⠀⠀⢿⣆⠀⠀⣨⣗⠂⠀⠀⠀⠀⠀⠀⠈⣇⠀⠀⠀⠀⠀
//              ⠀⠀⠀⠀⢸⡏⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⢠⡇⠀⢸⢸⢸⡇⠀⠘⣆⠀⠸⡄⠀⠀⠀⠀⠀⢸⣼⣤⠞⠁⢸⡄⢠⠀⠀⠀⠀⠈⣆⠘⡆⠀⠀⠀⠀
//              ⠀⠀⠀⠀⣸⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⡽⠓⢤⣾⢸⢸⡇⠀⠀⠸⡄⠀⣧⠀⠀⠀⠀⠀⣸⣿⡇⠀⠀⡼⢻⢸⠀⠀⠀⠀⠀⠘⡆⣿⠀⠀⠀⠀
//              ⠀⠀⠀⠀⡇⠀⠀⠀⠀⠀⠀⡇⠀⠀⠀⣰⠁⠀⣸⠋⡿⣾⡀⠀⠀⠀⢻⣰⢻⡆⢀⣀⣠⣶⣿⣥⣧⣤⣴⣃⠈⣿⠀⠀⠀⠀⠀⠀⣇⣿⠀⠀⠀⠀
//              ⠀⠀⠀⢀⡇⠀⠀⠀⠀⠀⣼⠁⠀⠀⡼⠃⢀⣴⣃⣴⣿⣥⣝⡲⠄⠀⠨⠟⠀⠓⠋⠉⠙⣿⠟⣻⣿⣭⡉⠻⢷⣿⠀⠀⠀⠀⠀⠀⣿⡿⠀⠀⠀⠀
//              ⠀⠀⠀⢸⠁⠀⠀⠀⠀⣰⠇⠀⢀⣼⡥⠖⣿⡿⢋⣻⣶⣶⣶⣍⠀⠀⠀⠀⠀⠀⠀⠀⠀⠉⣹⣁⣿⣿⣿⣆⠈⢻⣇⠀⠀⠀⠀⡼⠽⠁⠀⠀⠀⠀
//              ⠀⠀⠀⢸⠀⠀⠀⠀⢠⠏⢀⡴⠋⡼⠁⣼⠏⠀⣼⣁⣿⣿⣿⣿⣷⠀⠀⠀⠀⠀⠀⠀⠀⠀⣿⣿⣿⣿⣿⣿⠀⠀⣿⠀⠀⠠⢾⡁⠀⠀⠀⠀⠀⠀
//              ⠀⠀⠀⣿⠀⠀⠀⣰⠿⠚⠉⠀⢠⣧⣸⡟⠀⠀⣿⡟⢿⣿⣿⠏⣿⡇⠀⠀⠀⠀⠀⠀⠀⠀⠸⣷⣌⣿⣽⡾⠀⠀⠘⢇⠀⠀⠀⠉⠳⣄⠀⠀⠀⠀
//              ⠀⠀⠀⡿⠀⠀⠀⠀⠀⠀⠀⠀⠸⠁⠻⣧⠀⠀⢻⣧⣈⣍⣉⣴⡿⠀⠀⠀⠀⠀⠀⠀⠠⠀⠀⠈⠛⠛⠛⢁⣠⣤⣀⠈⢳⡄⠀⠀⠀⢹⡄⠀⠀⠀
//              ⠀⠀⢠⠇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⡇⠀⣀⣉⠛⠛⠛⠉⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⡼⢡⢿⣿⢏⡞⠀⢹⡀⠀⠀⢸⠃⠀⢀⠀
//              ⠀⠀⡼⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸⣴⡻⠋⣼⣽⣿⠃⢠⣄⣀⣀⣀⡤⠤⠶⣦⡤⠤⣴⣺⣿⠀⠛⠛⠛⠛⠀⠀⢸⠇⠀⠀⣼⠀⠀⠈⡆
//              ⠀⢰⠃⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸⠛⠁⠞⠟⠉⠀⠀⢸⠈⠛⠁⠀⠀⠀⠀⠀⠀⠘⠻⢷⡏⠀⠀⠀⠀⠀⠀⢠⠏⠀⠀⠀⠘⣄⣀⣴⡇
//              ⢀⡎⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸⡀⠀⠀⠀⠀⠀⠀⢸⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣸⠃⠀⠀⠀⠀⢀⣴⠋⠀⠐⢮⣶⣀⣀⡴⠛⠀
//              ⡼⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⢇⠀⠀⠳⣄⠈⠷⣄⣤⡞⠀⠀⠀⠸⣆⠀⠀⠀⠀⠀⠀⠀⠀⢀⡴⠃⠀⠀⠀⢀⡴⠛⠁⠀⠀⠀⠀⢀⡤⠟⠀⠀⠀
//              ⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠸⡄⠀⢀⡈⢙⣒⣶⣏⡀⠀⠀⠀⠀⠙⢦⣄⣀⠀⠀⣀⣤⠴⠋⠀⣀⣤⠔⠊⠉⠀⠀⠀⢠⣄⣰⡞⠉⠀⠀⠀⠀⠀
//              ⢳⡀⢧⢀⡀⠀⠀⠀⠀⠀⠀⠀⠙⢦⣄⡉⠉⣒⣮⣟⣉⣹⠶⣦⣄⠀⠀⠀⠉⣩⠹⢥⣶⣶⣯⣉⠁⠀⠀⠀⠀⠀⠀⢀⡼⠃⠉⠀⠀⠀⠀⠀⠀⠀
//              ⠀⠉⠉⠷⡝⠦⣄⡀⠀⠀⡀⠀⠀⠀⠀⠉⣽⡷⠋⠁⠘⡆⠀⠱⣌⠙⠒⠒⢚⣹⡀⠀⠙⢦⠈⠙⣷⣄⠀⠀⢀⣠⠴⠊⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
//              ⠀⠀⠀⠀⠀⠀⠀⠉⠉⠹⡧⠴⠲⢤⣤⣼⡟⠀⠀⠀⠀⢸⡄⠀⠀⠻⡭⠉⠉⠉⢟⡁⠀⠈⣆⠀⠈⡿⢷⠒⠉⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
//              ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠔⠉⠓⠲⠤⠤⠤⠼⠇⠀⠀⠪⠥⠀⠀⠀⠼⠏⠀⠀⠸⠖⠋⠁⠀⠳⠄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
//
// 
//                             yeah i use goto cry more
//

notWhatIAsked:

WriteLine("Would you like to locate your folder automatically or manually?");
WriteLine("");
WriteLine("1) Automatically");
WriteLine("    (ModelOptimizer will attempt to locate VTube Studio's model folder)", ConsoleColor.DarkGray);
WriteLine("2) Manually");
WriteLine("    (You will need to find the folder yourself.)", ConsoleColor.DarkGray);
WriteLine("3) How does this app work?");


chooseDirSearchMethod:

choice = Console.ReadLine()?.ToLower()?.Trim() ?? "`";

if (choice is "1")
{
    directoryPath = "C:\\Program Files (x86)\\Steam\\steamapps\\common\\VTube Studio\\VTube Studio_Data\\StreamingAssets\\Live2DModels";
    if (Directory.Exists(directoryPath))
    {
        WriteLine("Found VTube Studio at " + directoryPath + ".");
        goto gotdir;
    }

    WriteLine("Couldn't find VTube Studio! Please enter 2 to locate it manually.", ConsoleColor.Red);
    Write();

    goto chooseDirSearchMethod;
}
else if (choice is "2")
{
    Console.Clear();
    Console.WriteLine("\x1b[3J");
    Console.Clear();

    WriteLine(@"Please enter the path to the models. 
    All files under this path will be processed, and 
    it's a good idea to back up first to be safe.");

enterDirectory:

    directoryPath = Console.ReadLine()?.Trim() ?? "`";

    if (!Directory.Exists(directoryPath))
    {
        WriteLine("That directory doesn't seem to exist! Try again.", ConsoleColor.Red);
        Write();
        goto enterDirectory;
    }

    else
    {
        Console.Clear();
        WriteLine("Got it. Searching for suitable files...");

        DirectoryInfo di = new(directoryPath);
        FileInfo[] fis = di.GetFiles("*.png", new EnumerationOptions() { IgnoreInaccessible = true, RecurseSubdirectories = true, MatchCasing = MatchCasing.CaseInsensitive });

        WriteLine($@"Found {fis.Length} files to optimize, totaling {fis.Sum(x => x.Length) / MiB:0.00} MiB.");
        WriteLine("Use this directory?");

        WriteLine("1) Yes");
        WriteLine("2) No");

    enterDirectoryConfirmationPath1:

        choice = Console.ReadLine()?.ToLower()?.Trim() ?? "`";
        if (choice is "1" or "y" or "yes" or "yeah" or "sure" or "yeah whatever" or "absolutely bro")
        {
            WriteLine("Got it!");
        }
        else if (choice is "2" or "n" or "no" or "nah" or "nope" or "what the hell man why would you say that")
        {
            WriteLine("Going back to the path selection! Write a new folder path.");
            goto enterDirectory;
        }
        else
        {
            WriteLine("Inalid input. Please write 1 or 2.", ConsoleColor.Red);
            Write();
            goto enterDirectoryConfirmationPath1;
        }
    }
}
else if (choice is "3")
{
    Console.Clear();
    Console.WriteLine("\x1b[3J");
    Console.Clear();

    if (easterEgg is 1)
    {
        WriteLine("I already told you.", ConsoleColor.Red);
        Thread.Sleep(1500);
        WriteLine("Did you already forget?", ConsoleColor.Red);
        Thread.Sleep(1500);

        Console.Clear();
        Console.WriteLine("\x1b[3J");
        Console.Clear();
    }
    if (easterEgg is 2)
    {
        WriteLine("It literally says it on the screen before you type 3 what the hell man", ConsoleColor.Red);
        Thread.Sleep(1500);

        Console.Clear();
        Console.WriteLine("\x1b[3J");
        Console.Clear();
    }
    if (easterEgg is 3)
    {
        WriteLine("I quit", ConsoleColor.Red);
        Write();
        Thread.Sleep(1500);

        Environment.Exit(-69);
    }

    WriteLine("ModelOptimizer works by running OptiPNG on all of the model texture files in your VTube Studio data folder.");
    WriteLine("  \x1b[3mTechnically, you could use it on any folder...\x1b[23m", ConsoleColor.DarkGray);
    WriteLine();
    WriteLine("OptiPNG recompresses the data \x1b[4mlosslessly\x1b[24m, meaning that \x1b[4mnone of the color data is changed\x1b[24m.");
    WriteLine("It does this by trying a bunch of different compression methods. All of the recompression is done on your computer,");
    WriteLine("and it will take about 1-2 minutes per model (or for models with large texture atlases, 2-4 minutes.)");
    WriteLine();
    WriteLine("You should back up your models frequently. This app will copy your entire folder in an attempt to help you out");
    WriteLine("should anything go wrong. This backup is deleted on future runs.");
    WriteLine();
    WriteLine("All that said and done... ");
    WriteLine();

    easterEgg++;

    goto notWhatIAsked;
}
else
{
    WriteLine("Invalid input. Please enter 1, 2, or 3.", ConsoleColor.Red);
    Write();
    goto chooseDirSearchMethod;
}

gotdir:
;

Console.Clear();
Console.WriteLine("\x1b[3J");
Console.Clear();

//=====================================================================================================================
//
// Do the backup
//
//=====================================================================================================================

if (!Directory.Exists(directoryPath + ".modopt-backup"))
{
    WriteLine("Making a backup since you probably didn't...");
    WriteLine("    \x1b[3mDon't get angry with me, I'm trying to keep you safe..!\x1b[3m", ConsoleColor.DarkGray);
    WriteLine("");
}
else
{
    try
    {
        Directory.Delete(directoryPath + ".modopt-backup", true);
    }
    catch
    {

    }
}
retryBackup:

try
{
    CopyDirectory(directoryPath, directoryPath + ".modopt-backup", true);
}
catch
{
    WriteLine("Failed to create backup! Continue anyway?");

    WriteLine("1) Yes");

    WriteLine("2) No");

    WriteLine("3) Retry");

    WriteLine("4) I'll do it myself (opens File Explorer at the directory)");

    choice = Console.ReadLine()?.ToLower()?.Trim() ?? "`";

    if (choice is "1" or "yes" or "y")
    {
        WriteLine("Alrighty then!");
    }
    else if (choice is "2" or "no" or "n")
    {
        WriteLine("Exiting ModelOptimizer...");
        Environment.Exit(0);
    }
    else if (choice is "3" or "retry" or "r")
    {
        WriteLine("Alright. Retrying...");
        goto retryBackup;
    }
    else if (choice is "4" or "myself")
    {
        WriteLine("Alright, here you go. Come back and press ENTER when you're ready to continue.");
        Process.Start("explorer", $"\"{directoryPath}\"");
        Console.ReadLine();
    }
    else
    {
        WriteLine("That choice was not valid. Please enter a number between 1 and 4.", ConsoleColor.Red);
        goto retryBackup;
    }

}
WriteLine("Done!");

//=====================================================================================================================
//
// Actually do the work
//
//=====================================================================================================================

WriteLine("Getting file information...");

DirectoryInfo modelDir = new(directoryPath);

FileInfo[] modelImageFiles = modelDir.GetFiles("*.png", new EnumerationOptions()
{
    RecurseSubdirectories = true,
    IgnoreInaccessible = true,
    MatchCasing = MatchCasing.CaseInsensitive,
    MatchType = MatchType.Simple
});

string optiPngEffort = "-o2";

WriteLine($@"Found {modelImageFiles.Length} files, totaling {modelImageFiles.Sum(x => x.Length) / MiB:0.00}MiB.");

WriteLine($"OptiPNG Effort: {optiPngEffort}");
WriteLine($"  \x1b[3mThe tradeoff between effort and time past -o2 is not worth it (minutes for tens of bytes).\x1b[23m", ConsoleColor.DarkGray);
WriteLine($"Beginning operation...");
WriteLine();


float totalSizeBefore = 0.0f;
float totalSizeAfter = 0.0f;
int fileIndex = 0;
foreach (FileInfo modelImageFile in modelImageFiles)
{
    fileIndex++;
    WriteLine($"\u001b[0K\x1b[4m{modelImageFile.FullName.Replace(directoryPath, "~")}\x1b[24m", ConsoleColor.DarkGray);
    Write("  Was ");
    Write($"{modelImageFile.Length / MiB:0.00} MiB", ConsoleColor.Red);

    totalSizeBefore += modelImageFile.Length;

    (int l, int t) = Console.GetCursorPosition();

    WriteLine(", now ...");
    WriteLine();
    Write($"\x1b[3m  ({modelImageFiles.Length - fileIndex} files left)\x1b[23m", ConsoleColor.DarkGray);

    Process? p = Process.Start(
        new ProcessStartInfo("optipng.exe", $"\"{modelImageFile.FullName}\"")
        {
            CreateNoWindow = true,
            UseShellExecute = false,
            RedirectStandardError = true,
            RedirectStandardOutput = true,
            RedirectStandardInput = true
        });
    p?.WaitForExit();

    //Console.SetCursorPosition(l, t);
    //Thread.Sleep(50);

    modelImageFile.Refresh();

    //int magnitude = 1 + ($"{modelImageFiles.Length - fileIndex}".Length);

    Write($"\x1b[2A\x1b[{l + 1}G, now ");
    WriteLine($"{modelImageFile.Length / MiB:0.00} MiB", ConsoleColor.Green);
    WriteLine();

    totalSizeAfter += modelImageFile.Length;
}

Write();


//=====================================================================================================================
//
// Final Output
//
//=====================================================================================================================


Write($"Total Size Before: ");
WriteLine($"{totalSizeBefore / MiB:0.00} MiB", ConsoleColor.Red);

Write($"Total Size After:  ");
WriteLine($"{totalSizeAfter / MiB:0.00} MiB", ConsoleColor.Green);

Write($"Total Size Difference:  ");
WriteLine($"{(totalSizeBefore - totalSizeAfter )/ MiB:0.00} MiB", ConsoleColor.Yellow);

Write();

WriteLine("Press ENTER to exit.");

Console.ReadLine();

Environment.Exit(0);