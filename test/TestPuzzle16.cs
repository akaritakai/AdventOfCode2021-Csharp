using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2021;

[TestClass]
public class TestPuzzle16
{
    [TestMethod]
    public void TestSingleDigitInputParsing()
    {
        VerifyQueuesAreEqual(Puzzle16.ParseInput("0"), new Queue<bool>(new[] { false, false, false, false }));
        VerifyQueuesAreEqual(Puzzle16.ParseInput("1"), new Queue<bool>(new[] { false, false, false, true }));
        VerifyQueuesAreEqual(Puzzle16.ParseInput("2"), new Queue<bool>(new[] { false, false, true, false }));
        VerifyQueuesAreEqual(Puzzle16.ParseInput("3"), new Queue<bool>(new[] { false, false, true, true }));
        VerifyQueuesAreEqual(Puzzle16.ParseInput("4"), new Queue<bool>(new[] { false, true, false, false }));
        VerifyQueuesAreEqual(Puzzle16.ParseInput("5"), new Queue<bool>(new[] { false, true, false, true }));
        VerifyQueuesAreEqual(Puzzle16.ParseInput("6"), new Queue<bool>(new[] { false, true, true, false }));
        VerifyQueuesAreEqual(Puzzle16.ParseInput("7"), new Queue<bool>(new[] { false, true, true, true }));
        VerifyQueuesAreEqual(Puzzle16.ParseInput("8"), new Queue<bool>(new[] { true, false, false, false }));
        VerifyQueuesAreEqual(Puzzle16.ParseInput("9"), new Queue<bool>(new[] { true, false, false, true }));
        VerifyQueuesAreEqual(Puzzle16.ParseInput("A"), new Queue<bool>(new[] { true, false, true, false }));
        VerifyQueuesAreEqual(Puzzle16.ParseInput("B"), new Queue<bool>(new[] { true, false, true, true }));
        VerifyQueuesAreEqual(Puzzle16.ParseInput("C"), new Queue<bool>(new[] { true, true, false, false }));
        VerifyQueuesAreEqual(Puzzle16.ParseInput("D"), new Queue<bool>(new[] { true, true, false, true }));
        VerifyQueuesAreEqual(Puzzle16.ParseInput("E"), new Queue<bool>(new[] { true, true, true, false }));
        VerifyQueuesAreEqual(Puzzle16.ParseInput("F"), new Queue<bool>(new[] { true, true, true, true }));
    }

    [TestMethod]
    public void TestDoubleDigitInputParsing()
    {
        VerifyQueuesAreEqual(Puzzle16.ParseInput("00"), new Queue<bool>(new[] { false, false, false, false, false, false, false, false }));
        VerifyQueuesAreEqual(Puzzle16.ParseInput("22"), new Queue<bool>(new[] { false, false, true, false, false, false, true, false }));
    }

    private static void VerifyQueuesAreEqual(Queue<bool> first, Queue<bool> second)
    {
        Assert.AreEqual(first.Count, second.Count);
        while (first.Count > 0)
        {
            Assert.AreEqual(first.Dequeue(), second.Dequeue());
        }
    }

    [TestMethod]
    public void TestExamplePacket1()
    {
        const string input = "D2FE28";
        var packet = Puzzle16.ParsePacket(Puzzle16.ParseInput(input));
        Assert.AreEqual(packet.Version, 6);
        Assert.AreEqual(packet.TypeId, 4);
        Assert.AreEqual(packet.Payload, 2021);
    }
        
    [TestMethod]
    public void TestExamplePacket2()
    {
        const string input = "38006F45291200";
        var packet = Puzzle16.ParsePacket(Puzzle16.ParseInput(input));
        Assert.AreEqual(packet.Version, 1);
        Assert.AreEqual(packet.TypeId, 6);
        Assert.IsFalse(packet.LengthTypeId);
        Assert.AreEqual(packet.Payload, 27);
        Assert.AreEqual(packet.SubPackets.Count, 2);
        Assert.AreEqual(packet.SubPackets[0].TypeId, 4);
        Assert.AreEqual(packet.SubPackets[0].Payload, 10);
        Assert.AreEqual(packet.SubPackets[1].TypeId, 4);
        Assert.AreEqual(packet.SubPackets[1].Payload, 20);
    }
        
    [TestMethod]
    public void TestExamplePacket3()
    {
        const string input = "EE00D40C823060";
        var packet = Puzzle16.ParsePacket(Puzzle16.ParseInput(input));
        Assert.AreEqual(packet.Version, 7);
        Assert.AreEqual(packet.TypeId, 3);
        Assert.IsTrue(packet.LengthTypeId);
        Assert.AreEqual(packet.Payload, 3);
        Assert.AreEqual(packet.SubPackets.Count, 3);
        Assert.AreEqual(packet.SubPackets[0].TypeId, 4);
        Assert.AreEqual(packet.SubPackets[0].Payload, 1);
        Assert.AreEqual(packet.SubPackets[1].TypeId, 4);
        Assert.AreEqual(packet.SubPackets[1].Payload, 2);
        Assert.AreEqual(packet.SubPackets[2].TypeId, 4);
        Assert.AreEqual(packet.SubPackets[2].Payload, 3);
    }

    [TestMethod]
    public void TestExamplePacket4()
    {
        const string input = "8A004A801A8002F478";
        var packet = Puzzle16.ParsePacket(Puzzle16.ParseInput(input));
        Assert.AreEqual(packet.Version, 4);
        Assert.AreNotEqual(packet.TypeId, 4);
        Assert.AreEqual(packet.SubPackets.Count, 1);
        packet = packet.SubPackets[0];
        Assert.AreEqual(packet.Version, 1);
        Assert.AreNotEqual(packet.TypeId, 4);
        Assert.AreEqual(packet.SubPackets.Count, 1);
        packet = packet.SubPackets[0];
        Assert.AreEqual(packet.Version, 5);
        Assert.AreNotEqual(packet.TypeId, 4);
        Assert.AreEqual(packet.SubPackets.Count, 1);
        packet = packet.SubPackets[0];
        Assert.AreEqual(packet.Version, 6);
        Assert.AreEqual(packet.TypeId, 4);
        Assert.AreEqual(packet.SubPackets.Count, 0);
    }

    [TestMethod]
    public void TestExamplePacket5()
    {
        const string input = "620080001611562C8802118E34";
        var packet = Puzzle16.ParsePacket(Puzzle16.ParseInput(input));
        Assert.AreEqual(packet.Version, 3);
        Assert.AreNotEqual(packet.TypeId, 4);
        Assert.AreEqual(packet.SubPackets.Count, 2);
        Assert.AreNotEqual(packet.SubPackets[0].TypeId, 4);
        Assert.AreEqual(packet.SubPackets[0].SubPackets.Count, 2);
        Assert.IsTrue(packet.SubPackets[0].SubPackets.All(p => p.TypeId == 4));
        Assert.IsTrue(packet.SubPackets[0].SubPackets.All(p => p.SubPackets.Count == 0));
        Assert.AreNotEqual(packet.SubPackets[1].TypeId, 4);
        Assert.AreEqual(packet.SubPackets[1].SubPackets.Count, 2);
        Assert.IsTrue(packet.SubPackets[1].SubPackets.All(p => p.TypeId == 4));
        Assert.IsTrue(packet.SubPackets[1].SubPackets.All(p => p.SubPackets.Count == 0));
    }

    [TestMethod]
    public void TestExamplePacket6()
    {
        const string input = "C0015000016115A2E0802F182340";
        var packet = Puzzle16.ParsePacket(Puzzle16.ParseInput(input));
        Assert.AreNotEqual(packet.TypeId, 4);
        Assert.AreEqual(packet.SubPackets.Count, 2);
        Assert.AreNotEqual(packet.SubPackets[0].TypeId, 4);
        Assert.AreEqual(packet.SubPackets[0].SubPackets.Count, 2);
        Assert.IsTrue(packet.SubPackets[0].SubPackets.All(p => p.TypeId == 4));
        Assert.IsTrue(packet.SubPackets[0].SubPackets.All(p => p.SubPackets.Count == 0));
        Assert.AreNotEqual(packet.SubPackets[1].TypeId, 4);
        Assert.AreEqual(packet.SubPackets[1].SubPackets.Count, 2);
        Assert.IsTrue(packet.SubPackets[1].SubPackets.All(p => p.TypeId == 4));
        Assert.IsTrue(packet.SubPackets[1].SubPackets.All(p => p.SubPackets.Count == 0));
    }

    [TestMethod]
    public void TestExamplePacket7()
    {
        const string input = "A0016C880162017C3686B18A3D4780";
        var packet = Puzzle16.ParsePacket(Puzzle16.ParseInput(input));
        Assert.AreNotEqual(packet.TypeId, 4);
        Assert.AreEqual(packet.SubPackets.Count, 1);
        packet = packet.SubPackets[0];
        Assert.AreNotEqual(packet.TypeId, 4);
        Assert.AreEqual(packet.SubPackets.Count, 1);
        packet = packet.SubPackets[0];
        Assert.AreNotEqual(packet.TypeId, 4);
        Assert.AreEqual(packet.SubPackets.Count, 5);
        Assert.IsTrue(packet.SubPackets.All(p => p.TypeId == 4));
        Assert.IsTrue(packet.SubPackets.All(p => p.SubPackets.Count == 0));
    }
        
    [TestMethod]
    public void TestPart1Example1()
    {
        var puzzle = new Puzzle16("8A004A801A8002F478");
        Assert.AreEqual("16", puzzle.SolvePart1());
    }
        
    [TestMethod]
    public void TestPart1Example2()
    {
        var puzzle = new Puzzle16("620080001611562C8802118E34");
        Assert.AreEqual("12", puzzle.SolvePart1());
    }
        
    [TestMethod]
    public void TestPart1Example3()
    {
        var puzzle = new Puzzle16("C0015000016115A2E0802F182340");
        Assert.AreEqual("23", puzzle.SolvePart1());
    }
        
    [TestMethod]
    public void TestPart1Example4()
    {
        var puzzle = new Puzzle16("A0016C880162017C3686B18A3D4780");
        Assert.AreEqual("31", puzzle.SolvePart1());
    }

    [TestMethod]
    public void TestSolvePart1()
    {
        var input = BasePuzzleTest.PuzzleInput(16);
        var puzzle = new Puzzle16(input);
        Assert.AreEqual("883", puzzle.SolvePart1());
    }

    [TestMethod]
    public void TestPart2Example1()
    {
        var puzzle = new Puzzle16("C200B40A82");
        Assert.AreEqual("3", puzzle.SolvePart2());
    }
        
    [TestMethod]
    public void TestPart2Example2()
    {
        var puzzle = new Puzzle16("04005AC33890");
        Assert.AreEqual("54", puzzle.SolvePart2());
    }
        
    [TestMethod]
    public void TestPart2Example3()
    {
        var puzzle = new Puzzle16("880086C3E88112");
        Assert.AreEqual("7", puzzle.SolvePart2());
    }
        
    [TestMethod]
    public void TestPart2Example4()
    {
        var puzzle = new Puzzle16("CE00C43D881120");
        Assert.AreEqual("9", puzzle.SolvePart2());
    }
        
    [TestMethod]
    public void TestPart2Example5()
    {
        var puzzle = new Puzzle16("D8005AC2A8F0");
        Assert.AreEqual("1", puzzle.SolvePart2());
    }
        
    [TestMethod]
    public void TestPart2Example6()
    {
        var puzzle = new Puzzle16("F600BC2D8F");
        Assert.AreEqual("0", puzzle.SolvePart2());
    }
        
    [TestMethod]
    public void TestPart2Example7()
    {
        var puzzle = new Puzzle16("9C005AC2F8F0");
        Assert.AreEqual("0", puzzle.SolvePart2());
    }
        
    [TestMethod]
    public void TestPart2Example8()
    {
        var puzzle = new Puzzle16("9C0141080250320F1802104A08");
        Assert.AreEqual("1", puzzle.SolvePart2());
    }

    [TestMethod]
    public void TestSolvePart2()
    {
        var input = BasePuzzleTest.PuzzleInput(16);
        var puzzle = new Puzzle16(input);
        Assert.AreEqual("1675198555015", puzzle.SolvePart2());
    }
}