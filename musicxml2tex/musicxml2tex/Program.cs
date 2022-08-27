
using MusicXmlSchema;
using System.Xml;
using System.Xml.Serialization;

var inputDir = "./input";


foreach (var file in Directory.GetFiles(inputDir))
{
    var xmlStream = File.OpenRead(file);

    var reader = XmlReader.Create(xmlStream, new XmlReaderSettings() { ConformanceLevel = ConformanceLevel.Document, DtdProcessing = DtdProcessing.Ignore });
    var obj = new XmlSerializer(typeof(ScorePartwise)).Deserialize(reader) as ScorePartwise;

    var part = obj.Part.First();


    var measures = part.Measure;

    var wordPart = string.Empty;

    var lastChord = string.Empty;

    var result = string.Empty;

    foreach (var measure in measures)
    {
        if (measure.Print.FirstOrDefault()?.NewSystem == YesNo.Yes)
        {
            result += "\n";
        }

        var chord = GetHarmony(measure);

        if (chord != null && lastChord != chord)
        {
            result += $"\\[{chord}]";
            lastChord = chord;
        }

        foreach (var note in measure.Note)
        {
            foreach (var lyric in note.Lyric)
            {
                if (lyric.Syllabic == Syllabic.Single)
                {
                    result += lyric.Text.Value + " ";
                    continue;
                }

                wordPart += lyric.Text.Value;

                if (lyric.Syllabic == Syllabic.End)
                {
                    result += wordPart + " ";
                    wordPart = string.Empty;
                }
            }
        }
    }

    Console.WriteLine(result);
}

static string GetHarmony(ScorePartwisePartMeasure? measure)
{
    var harmony = measure.Harmony.FirstOrDefault();

    if (harmony == null)
        return null;

    var root = harmony.Root.First();

    var chord = root.RootStep.Value;

    var chordStr = chord.ToString();

    if (harmony.Kind.Count > 1)
        throw new NotSupportedException();

    var kind = harmony.Kind.First();

    if (kind.Value == KindValue.Major)
    {
    }
    else if (kind.Value == KindValue.Minor)
    {
        chordStr += "m";
    }
    else if (kind.Value == KindValue.Dominant)
    {
        chordStr += "7";
    }
    else
    {
        throw new NotSupportedException();
    }

    return chordStr;
}