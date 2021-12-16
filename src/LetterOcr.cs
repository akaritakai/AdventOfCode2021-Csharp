using System.Text;

namespace AdventOfCode2021;

public class LetterOcr
{
    private const int LetterWidth = 4;
    private const int LetterHeight = 6;

    private static readonly IDictionary<char, bool[,]> Letters = new Dictionary<char, bool[,]>
    {
        {'A', new[,]
            {
                {false, true, true, false},
                {true, false, false, true},
                {true, false, false, true}, 
                {true, true, true, true}, 
                {true, false, false, true}, 
                {true, false, false, true}
            }
        },
        {'B', new[,]
            {
                {true, true, true, false},
                {true, false, false, true},
                {true, true, true, false},
                {true, false, false, true},
                {true, false, false, true},
                {true, true, true, false}
            }
        },
        {'C', new[,]
            {
                {false, true, true, false},
                {true, false, false, true},
                {true, false, false, false},
                {true, false, false, false},
                {true, false, false, true},
                {false, true, true, false}
            }
        },
        // Letter D is unknown
        {'E', new[,]
            {
                {true, true, true, true},
                {true, false, false, false},
                {true, true, true, false},
                {true, false, false, false},
                {true, false, false, false},
                {true, true, true, true}
            }
        },
        {'F', new[,]
            {
                {true, true, true, true},
                {true, false, false, false},
                {true, true, true, false},
                {true, false, false, false},
                {true, false, false, false},
                {true, false, false, false}
            }
        },
        {'G', new[,]
            {
                {false, true, true, false},
                {true, false, false, true},
                {true, false, false, false},
                {true, false, true, true},
                {true, false, false, true},
                {false, true, true, true}
            }
        },
        {'H', new[,]
            {
                {true, false, false, true},
                {true, false, false, true},
                {true, true, true, true},
                {true, false, false, true},
                {true, false, false, true},
                {true, false, false, true}
            }
        },
        {'I', new[,]
            {
                {false, true, true, true},
                {false, false, true, false},
                {false, false, true, false},
                {false, false, true, false},
                {false, false, true, false},
                {false, true, true, true}
            }
        },
        {'J', new[,]
            {
                {false, false, true, true},
                {false, false, false, true},
                {false, false, false, true},
                {false, false, false, true},
                {true, false, false, true},
                {false, true, true, false}
            }
        },
        {'K', new[,]
            {
                {true, false, false, true},
                {true, false, true, false},
                {true, true, false, false},
                {true, false, true, false},
                {true, false, true, false},
                {true, false, false, true}
            }
        },
        {'L', new[,]
            {
                {true, false, false, false},
                {true, false, false, false},
                {true, false, false, false},
                {true, false, false, false},
                {true, false, false, false},
                {true, true, true, true}
            }
        },
        // Letter M is unknown
        // Letter N is unknown
        {'P', new[,]
            {
                {true, true, true, false},
                {true, false, false, true},
                {true, false, false, true},
                {true, true, true, false},
                {true, false, false, false},
                {true, false, false, false}
            }
        },
        // Letter Q is unknown
        {'R', new[,]
            {
                {true, true, true, false},
                {true, false, false, true},
                {true, false, false, true},
                {true, true, true, false},
                {true, false, true, false},
                {true, false, false, true}
            }
        },
        {'S', new[,]
            {
                {false, true, true, true},
                {true, false, false, false},
                {true, false, false, false},
                {false, true, true, false},
                {false, false, false, true},
                {true, true, true, false}
            }
        },
        // Letter T is unknown
        {'U', new[,]
            {
                {true, false, false, true},
                {true, false, false, true},
                {true, false, false, true},
                {true, false, false, true},
                {true, false, false, true},
                {false, true, true, false}
            }
        },
        // Letter V is unknown
        // Letter W is unknown
        // Letter X is unknown
        {'Y', new[,]
            {
                {true, false, false, false},
                {true, false, false, false},
                {false, true, false, true},
                {false, false, true, false},
                {false, false, true, false},
                {false, false, true, false}
            }
        },
        {'Z', new[,]
            {
                {true, true, true, true},
                {false, false, false, true},
                {false, false, true, false},
                {false, true, false, false},
                {true, false, false, false},
                {true, true, true, true}
            }
        }
    };

    private static char ParseLetter(bool[,] image, int rowOffset, int colOffset)
    {
        foreach (var (letter, pattern) in Letters)
        {
            var allMatch = true;
            for (var row = 0; row < LetterHeight; row++)
            {
                for (var col = 0; col < LetterWidth; col++)
                {
                    try
                    {
                        if (image[rowOffset + row, colOffset + col] == pattern[row, col]) continue;
                        allMatch = false;
                        goto FINISHED_CHECKING_PATTERN;
                    }
                    catch (Exception)
                    {
                        throw new OcrException("Issue parsing letter");
                    }
                }
            }
            FINISHED_CHECKING_PATTERN:
            if (allMatch)
            {
                return letter;
            }
        }
        throw new OcrException("Unrecognized letter");
    }

    public static string Parse(bool[,] image)
    {
        try
        {
            var sb = new StringBuilder();
            for (var col = 0; col <= image.GetLength(1) - LetterWidth; col += LetterWidth + 1)
            {
                sb.Append(ParseLetter(image, 0, col));
            }

            return sb.ToString();
        }
        catch (Exception)
        {
            var sb = new StringBuilder();
            sb.Append('\n');
            for (var row = 0; row < image.GetLength(0); row++)
            {
                for (var col = 0; col < image.GetLength(1); col++)
                {
                    sb.Append(image[row, col] ? '▌' : ' ');
                }
                sb.Append('\n');
            }
            sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }
    }
}

public class OcrException : Exception
{
    public OcrException(string message) : base(message)
    {
    }
}