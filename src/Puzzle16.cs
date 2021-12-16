namespace AdventOfCode2021;

public class Puzzle16 : AbstractPuzzle
{
    public Puzzle16(string input) : base(input)
    {
    }

    public override int Day()
    {
        return 16;
    }

    public override string SolvePart1()
    {
        var packet = ParsePacket(ParseInput(Input));
        return VersionSum(packet).ToString();
    }

    public override string SolvePart2()
    {
        var packet = ParsePacket(ParseInput(Input));
        return Evaluate(packet).ToString();
    }

    private static long VersionSum(Packet packet)
    {
        return packet.Version + packet.SubPackets.Sum(VersionSum);
    }

    private static long Evaluate(Packet packet)
    {
        switch (packet.TypeId)
        {
            case 0: return packet.SubPackets.Select(Evaluate).Sum();
            case 1: return packet.SubPackets.Select(Evaluate).Aggregate(1L, (a, b) => a * b);
            case 2: return packet.SubPackets.Select(Evaluate).Min();
            case 3: return packet.SubPackets.Select(Evaluate).Max();
            case 4: return packet.Payload;
            case 5: return Evaluate(packet.SubPackets[0]) > Evaluate(packet.SubPackets[1]) ? 1L : 0L;
            case 6: return Evaluate(packet.SubPackets[0]) < Evaluate(packet.SubPackets[1]) ? 1L : 0L;
            case 7: return Evaluate(packet.SubPackets[0]) == Evaluate(packet.SubPackets[1]) ? 1L : 0L;
        }
        throw new Exception("Unknown packet type");
    }

    public static Queue<bool> ParseInput(string input)
    {
        var result = new List<bool>();
        foreach (var c in input.Trim())
        {
            switch (c)
            {
                case '0': result.AddRange(new[] { false, false, false, false }); break;
                case '1': result.AddRange(new[] { false, false, false, true }); break;
                case '2': result.AddRange(new[] { false, false, true, false }); break;
                case '3': result.AddRange(new[] { false, false, true, true }); break;
                case '4': result.AddRange(new[] { false, true, false, false }); break;
                case '5': result.AddRange(new[] { false, true, false, true }); break;
                case '6': result.AddRange(new[] { false, true, true, false }); break;
                case '7': result.AddRange(new[] { false, true, true, true }); break;
                case '8': result.AddRange(new[] { true, false, false, false }); break;
                case '9': result.AddRange(new[] { true, false, false, true }); break;
                case 'A': result.AddRange(new[] { true, false, true, false }); break;
                case 'B': result.AddRange(new[] { true, false, true, true }); break;
                case 'C': result.AddRange(new[] { true, true, false, false }); break;
                case 'D': result.AddRange(new[] { true, true, false, true }); break;
                case 'E': result.AddRange(new[] { true, true, true, false }); break;
                case 'F': result.AddRange(new[] { true, true, true, true }); break;
            }
        }
        var queue = new Queue<bool>(result);
        return queue;
    }

    public static Packet ParsePacket(Queue<bool> input)
    {
        var version = ReadBits(input, 3);
        var typeId = ReadBits(input, 3);
        if (typeId == 4)
        {
            long payload = 0;
            while (true)
            {
                var x = ReadBits(input, 5);
                payload <<= 4;
                payload |= x & 15;
                if ((x & 16) == 0)
                {
                    break;
                }
            }
            return new Packet(version, typeId, false, payload, new List<Packet>());
        }
        else
        {
            var lengthTypeId = ReadBits(input, 1) == 1;
            long payload;
            var subPackets = new List<Packet>();
            if (lengthTypeId)
            {
                payload = ReadBits(input, 11);
                for (var i = 0; i < payload; i++)
                {
                    subPackets.Add(ParsePacket(input));
                }
            }
            else
            {
                payload = ReadBits(input, 15);
                var payloadBits = new Queue<bool>();
                for (var i = 0; i < payload; i++)
                {
                    payloadBits.Enqueue(input.Dequeue());
                }
                while (payloadBits.Count > 0)
                {
                    subPackets.Add(ParsePacket(payloadBits));
                }
            }
            return new Packet(version, typeId, lengthTypeId, payload, subPackets);
        }
    }

    private static long ReadBits(Queue<bool> input, int size)
    {
        var result = 0;
        for (var i = 0; i < size; i++)
        {
            result <<= 1;
            if (input.Dequeue())
            {
                result |= 1;
            }
        }
        return result;
    }

    public readonly struct Packet
    {
        public Packet(long version, long typeId, bool lengthTypeId, long payload, List<Packet> subPackets)
        {
            Version = version;
            TypeId = typeId;
            LengthTypeId = lengthTypeId;
            Payload = payload;
            SubPackets = subPackets;
        }
            
        public long Version { get; }
        public long TypeId { get; }
        public bool LengthTypeId { get; }
        public long Payload { get; }
        public List<Packet> SubPackets { get; }
    }
}