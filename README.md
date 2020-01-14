## ExtraUtils.Randomizer

This library contains the ``RNG`` struct that provides pseudorandom
number using **shift-register generators** or **xorshift**.

------------------
### Implementation

```csharp
public struct RNG
{
    // A default RNG with a random seed
    public static RNG Default { get; }

    // Constructors
    public RNG(uint seed);
    public RNG(int seed);

    // Pseudorandom int generators
    public int NextInt();
    public int NextInt(int max);
    public int NextInt(int min, int max);

    // Pseudorandom long generators
    public long NextLong();
    public long NextLong(long max);
    public long NextLong(long min, long min);

    // Pseudorandom uint generators
    public uint NextUInt();
    public uint NextUInt(uint max);
    public uint NextUInt(uint min, uint max);

    // Pseudorandom ulong generators
    public ulong NextULong();
    public ulong NextULong(ulong max);
    public ulong NextULong(ulong min, ulong max);

    // Pseudorandom float generators
    public float NextFloat();
    public float NextFloat(float max);
    public float NextFloat(float min, float max);

    // Pseudorandom double generators
    public double NextDouble();
    public double NextDouble(double max);
    public double NextDouble(double min, double max);   

    // Pseudorandom bool generator
    public bool NextBool();

    // Pseudorandom bit generator
    public int NextBits(int bitsCount);

    // Pseudorandom char generators
    public char NextChar();
    public char NextChar(RandomCharKind charKind);
    
    // Pseudorandom buffer generators
    public NextString(Span<char> destination);
    public NextString(Span<char> destination, RandomCharKind charKind);
    public void NextBytes(Span<byte> destination); 
}
```