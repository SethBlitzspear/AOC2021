using AOC2021_Day22;

string[] Puzzleinput = File.ReadAllLines("Input.txt");

bool[,,] ReactorCore = new bool[101, 101, 101];
List<Cube> BootCubes = new List<Cube>();

for(int bootCount = 0; bootCount < Puzzleinput.Length; bootCount++)
{
    string bootItem = Puzzleinput[bootCount];
    string bootCoords = bootItem.Split(' ')[1];
    string bootCommand = bootItem.Split(' ')[0];

    string bootXCoords = bootCoords.Split(',')[0].Substring(2, bootCoords.Split(',')[0].Length - 2);
    string bootYCoords = bootCoords.Split(',')[1].Substring(2, bootCoords.Split(',')[1].Length - 2);
    string bootZCoords = bootCoords.Split(',')[2].Substring(2, bootCoords.Split(',')[2].Length - 2);

    long XMin = Math.Min(Convert.ToInt64(bootXCoords.Split("..")[0]), Convert.ToInt64(bootXCoords.Split("..")[1]));
    long XMax = Math.Max(Convert.ToInt64(bootXCoords.Split("..")[0]), Convert.ToInt64(bootXCoords.Split("..")[1]));

    long YMin = Math.Min(Convert.ToInt64(bootYCoords.Split("..")[0]), Convert.ToInt64(bootYCoords.Split("..")[1]));
    long YMax = Math.Max(Convert.ToInt64(bootYCoords.Split("..")[0]), Convert.ToInt64(bootYCoords.Split("..")[1]));

    long ZMin = Math.Min(Convert.ToInt64(bootZCoords.Split("..")[0]), Convert.ToInt64(bootZCoords.Split("..")[1]));
    long ZMax = Math.Max(Convert.ToInt64(bootZCoords.Split("..")[0]), Convert.ToInt64(bootZCoords.Split("..")[1]));

    

    bool bootSetting = false;
    if (bootCommand == "on") // On
    {
        bootSetting = true;
    }

    if (BootCubes.Count() == 0)
    {
        BootCubes.Add(new Cube(XMin, XMax, YMin, YMax, ZMin, ZMax));
    }
    else
    {
        if (bootSetting)
        {
            List<Cube> AddCubes = new List<Cube>();
            AddCubes.Add(new Cube(XMin, XMax, YMin, YMax, ZMin, ZMax));
            foreach (Cube bootCube in BootCubes)
            {
                List<Cube> newAddCubes = new List<Cube>();
                foreach (Cube addCube in AddCubes)
                {
                    Cube[] fracturedCubes = addCube.CompareCuboids(bootCube);
                    if (fracturedCubes == null)
                    {
                        newAddCubes.Add(addCube);
                    }
                    else
                    {
                        newAddCubes.AddRange(fracturedCubes);
                    }
                }
                AddCubes = newAddCubes;
            }
            BootCubes.AddRange(AddCubes);
        }
        else
        {
            Cube deleteCube = new Cube(XMin, XMax, YMin, YMax, ZMin, ZMax);

            List<Cube> newBootCubes = new List<Cube>();
            foreach (Cube bootCube in BootCubes)
            {
                Cube[] fracturedCubes = bootCube.CompareCuboids(deleteCube);
                if (fracturedCubes == null)
                {
                    newBootCubes.Add(bootCube);
                }
                else
                {
                    newBootCubes.AddRange(fracturedCubes);
                }
            }
                BootCubes = newBootCubes;
        }
    }
    long fullReactorTotal = 0;
    foreach (Cube bootCube in BootCubes)
    {
        fullReactorTotal += bootCube.Volume;
    }
    Console.WriteLine("Proceesed boot line " + bootCount + "/" + Puzzleinput.Length + ". Tracking " + BootCubes.Count + " boot Cuboids" + " with power of " + fullReactorTotal);
    XMin = Math.Max(XMin, -50);
    XMax = Math.Min(XMax, 50);
    YMin = Math.Max(YMin, -50);
    YMax = Math.Min(YMax, 50);
    ZMin = Math.Max(ZMin, -50);
    ZMax = Math.Min(ZMax, 50);

    for (long xCount = XMin; xCount <= XMax; xCount++)
    {
        for (long yCount = YMin; yCount <= YMax; yCount++)
        {
            for (long zCount = ZMin; zCount <= ZMax; zCount++)
            {
                ReactorCore[xCount + 50, yCount + 50, zCount + 50] = bootSetting;
            }
        }
    }

    
}
int reactorTotal = 0;
for (int xCount = 0; xCount < 101; xCount++)
{
    for (int yCount = 0; yCount < 101; yCount++)
    {
        for (int zCount = 0; zCount < 101; zCount++)
        {
            if (ReactorCore[xCount, yCount, zCount] == true)
            {
                reactorTotal++;
            }
        }
    }
}
Console.WriteLine(reactorTotal);

/*long fullReactorTotal = 0;
foreach (Cube bootCube in BootCubes)
{
    fullReactorTotal += bootCube.Volume;
}
Console.WriteLine("Full amount " + fullReactorTotal);*/
namespace AOC2021_Day22
{
    public class Cube
    {

        long xMin, xMax, yMin, yMax, zMin, zMax;
        List<Fracture> xFractures = new List<Fracture>();
        List<Fracture> yFractures = new List<Fracture>();
        List<Fracture> zFractures = new List<Fracture>();

        public long XMin { get => xMin; set => xMin = value; }
        public long XMax { get => xMax; set => xMax = value; }
        public long YMin { get => yMin; set => yMin = value; }
        public long YMax { get => yMax; set => yMax = value; }
        public long ZMin { get => zMin; set => zMin = value; }
        public long ZMax { get => zMax; set => zMax = value; }
        public List<Fracture> XFractures { get => xFractures; set => xFractures = value; }
        public List<Fracture> YFractures { get => yFractures; set => yFractures = value; }
        public List<Fracture> ZFractures { get => zFractures; set => zFractures = value; }

        public long Volume
        {
            get
            {
                return (XMax - XMin + 1) * (YMax - YMin + 1) * (ZMax - ZMin + 1);
            }
        }

        public bool ContainsCube(Cube innerCube)
        {
            if (innerCube.XMin >= XMin && innerCube.XMin <= XMax)
            {
                if (innerCube.XMax >= XMin && innerCube.XMax <= XMax)
                {
                    if (innerCube.YMin >= YMin && innerCube.YMin <= YMax)
                    {
                        if (innerCube.YMax >= YMin && innerCube.YMax <= YMax)
                        {
                            if (innerCube.ZMin >= ZMin && innerCube.ZMin <= ZMax)
                            {
                                if (innerCube.ZMax >= ZMin && innerCube.ZMax <= ZMax)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }
        public bool ContainsPartCube(Cube innerCube)
        {
            if (innerCube.XMin >= XMin && innerCube.XMin <= XMax)
            {
                return true;
            }
            if (innerCube.XMax >= XMin && innerCube.XMax <= XMax)
            {
                return true;
            }
            if (innerCube.YMin >= YMin && innerCube.YMin <= YMax)
            {
                return true;
            }
            if (innerCube.YMax >= YMin && innerCube.YMax <= YMax)
            {
                return true;
            }
            if (innerCube.ZMin >= ZMin && innerCube.ZMin <= ZMax)
            {
                return true;
            }
            if (innerCube.ZMax >= ZMin && innerCube.ZMax <= ZMax)
            {
                return true;
            }
            return false;
        }
        public Cube(long newXMin, long newXMax, long newYMin, long newYMax, long newZMin, long newZMax)
        {
            XMin = newXMin;
            YMin = newYMin;
            ZMin = newZMin;

            XMax = newXMax;
            YMax = newYMax;
            ZMax = newZMax;

            

        }


        public Cube[] CompareCuboids(Cube foriegnCube)
        {
            xFractures = new List<Fracture>();
            yFractures = new List<Fracture>();
            zFractures = new List<Fracture>();

            xFractures.Add(new Fracture() { Pos = XMin, Min = true });
            xFractures.Add(new Fracture() { Pos = XMax, Min = false });
            yFractures.Add(new Fracture() { Pos = YMin, Min = true });
            yFractures.Add(new Fracture() { Pos = YMax, Min = false });
            zFractures.Add(new Fracture() { Pos = ZMin, Min = true });
            zFractures.Add(new Fracture() { Pos = ZMax, Min = false });
            bool Overlap = false;

            bool xMinIn = false, xMaxIn = false, yMinIn = false, yMaxIn = false, zMinIn = false, zMaxIn = false;
            if (foriegnCube.XMin >= XMin && foriegnCube.XMin <= XMax)
            {
                xMinIn = true;
            }
            if (foriegnCube.XMax >= XMin && foriegnCube.XMax <= XMax)
            {
                xMaxIn = true;
            }
            if (foriegnCube.YMin >= YMin && foriegnCube.YMin <= YMax)
            {
                yMinIn = true;
            }
            if (foriegnCube.YMax >= YMin && foriegnCube.YMax <= YMax)
            {
                yMaxIn = true;
            }
            if (foriegnCube.ZMin >= ZMin && foriegnCube.ZMin <= ZMax)
            {
                zMinIn = true;
            }
            if (foriegnCube.ZMax >= ZMin && foriegnCube.ZMax <= ZMax)
            {
                zMaxIn = true;
            }

            bool xMinFracture = false, xMaxFracture = false, yMinFracture = false, yMaxFracture = false, zMinFracture = false, zMaxFracture = false;

            if (xMinIn && yMinIn && zMinIn)
            {
                xMinFracture = yMinFracture = zMinFracture = Overlap = true;
            }
            if (xMinIn && yMinIn && zMaxIn)
            {
                xMinFracture = yMinFracture = zMaxFracture = Overlap = true;
            }
            if (xMinIn && yMaxIn && zMinIn)
            {
                xMinFracture = yMaxFracture = zMinFracture = Overlap = true;
            }
            if (xMinIn && yMaxIn && zMaxIn)
            {
                xMinFracture = yMaxFracture = zMaxFracture = Overlap = true;
            }
            if (xMaxIn && yMinIn && zMinIn)
            {
                xMaxFracture = yMinFracture = zMinFracture = Overlap = true;
            }
            if (xMaxIn && yMinIn && zMaxIn)
            {
                xMaxFracture = yMinFracture = zMaxFracture = Overlap = true;
            }
            if (xMaxIn && yMaxIn && zMinIn)
            {
                xMaxFracture = yMaxFracture = zMinFracture = Overlap = true;
            }
            if (xMaxIn && yMaxIn && zMaxIn)
            {
                xMaxFracture = yMaxFracture = zMaxFracture = Overlap = true;
            }

            if (xMinFracture && !(foriegnCube.XMin == XMin || foriegnCube.XMin == XMax))
            {
                XFractures.Add(new Fracture() { Pos = foriegnCube.XMin, Min = true });
            }
            if (xMaxFracture && !(foriegnCube.XMax == XMin || foriegnCube.XMax == XMax))
            {
                XFractures.Add(new Fracture() { Pos = foriegnCube.XMax, Min = false });
            }
            if (yMinFracture && !(foriegnCube.YMin == YMin || foriegnCube.YMin == YMax))
            {
                YFractures.Add(new Fracture() { Pos = foriegnCube.YMin, Min = true });
            }
            if (yMaxFracture && !(foriegnCube.YMax == YMin || foriegnCube.YMax == YMax))
            {
                YFractures.Add(new Fracture() { Pos = foriegnCube.YMax, Min = false });
            }
            if (zMinFracture && !(foriegnCube.ZMin == ZMin || foriegnCube.ZMin == ZMax))
            {
                ZFractures.Add(new Fracture() { Pos = foriegnCube.ZMin, Min = true });
            }
            if (zMaxFracture && !(foriegnCube.ZMax == ZMin || foriegnCube.ZMax == ZMax))
            { 
                ZFractures.Add(new Fracture() { Pos = foriegnCube.ZMax, Min = false});
            }
            /*
            if (foriegnCube.XMin >= XMin && foriegnCube.XMin <= XMax)
            {
                Overlap = true;
                if (!XFractures.Contains(foriegnCube.XMin))
                {
                    XFractures.Add(foriegnCube.XMin);
                }
            }
            if (foriegnCube.XMax >= XMin && foriegnCube.XMax <= XMax)
            {
                Overlap = true;
                if (!XFractures.Contains(foriegnCube.XMax))
                {
                    XFractures.Add(foriegnCube.XMax);
                }
            }
            if (foriegnCube.YMin >= YMin && foriegnCube.YMin <= YMax)
            {
                Overlap = true;
                if (!YFractures.Contains(foriegnCube.YMin))
                {
                    YFractures.Add(foriegnCube.YMin);
                }
            }
            if (foriegnCube.YMax >= YMin && foriegnCube.YMax <= YMax)
            {
                Overlap = true;
                if (!YFractures.Contains(foriegnCube.YMax))
                {
                    YFractures.Add(foriegnCube.YMax);
                }
            }
            if (foriegnCube.ZMin >= ZMin && foriegnCube.ZMin <= ZMax)
            {
                Overlap = true;
                if (!ZFractures.Contains(foriegnCube.ZMin))
                {
                    ZFractures.Add(foriegnCube.ZMin);
                }
            }
            if (foriegnCube.ZMax >= ZMin && foriegnCube.ZMax <= ZMax)
            {
                Overlap = true;
                if (!ZFractures.Contains(foriegnCube.ZMax))
                {
                    ZFractures.Add(foriegnCube.ZMax);
                }
            }*/

            if (!Overlap)
            {
                return null;
            }
            XFractures.Sort((x, y) => x.Pos.CompareTo(y.Pos));
            YFractures.Sort((x, y) => x.Pos.CompareTo(y.Pos));
            ZFractures.Sort((x, y) => x.Pos.CompareTo(y.Pos));

            List<Cube> fractures = new List<Cube>();

            for (int xCount = 0; xCount < XFractures.Count - 1; xCount++)
            {
                for (int yCount = 0; yCount < YFractures.Count - 1; yCount++)
                {
                    for (int zCount = 0; zCount < ZFractures.Count - 1; zCount++)
                    {
                        long newXMax = XFractures[xCount + 1].Min ? XFractures[xCount + 1].Pos - 1 : XFractures[xCount + 1].Pos;
                        long newYMax = YFractures[yCount + 1].Min ? YFractures[yCount + 1].Pos - 1 : YFractures[yCount + 1].Pos;
                        long newZMax = ZFractures[zCount + 1].Min ? ZFractures[zCount + 1].Pos - 1 : ZFractures[zCount + 1].Pos;

                        long newXMin = XFractures[xCount].Min ? XFractures[xCount].Pos : XFractures[xCount].Pos + 1; // xCount == 0 ? XFractures[xCount] : XFractures[xCount] + 1;
                        long newYMin = YFractures[yCount].Min ? YFractures[yCount].Pos : YFractures[yCount].Pos + 1; // yCount == 0 ? YFractures[yCount] : YFractures[yCount] + 1;
                        long newZMin = ZFractures[zCount].Min ? ZFractures[zCount].Pos : ZFractures[zCount].Pos + 1; // zCount == 0 ? ZFractures[zCount] : ZFractures[zCount] + 1;

                        Cube fractureCube = new Cube(newXMin, newXMax, newYMin, newYMax, newZMin, newZMax);
                        if (!foriegnCube.ContainsCube(fractureCube))
                        {
                            fractures.Add(fractureCube);
                        }
                    }
                }
            }
            return fractures.ToArray();
        }
    }
}

public struct Fracture
{
    long pos;
    bool min;

    public long Pos { get => pos; set => pos = value; }
    public bool Min { get => min; set => min = value; }
}