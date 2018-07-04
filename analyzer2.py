from line_analyzer import analyzeLine
from phonetics import transcribe
from phonetics import truncateEnd
from rules import RulesA
from rules import RulesB


class Entry:

    def GetLine(self, separator="|"):
        s = separator
        return self.lemma + s + self.prepForm + s + self.partOfSpeech + s + self.case + s + self.categories + s + self.transcription + s + self.voc + s + str(self.no) + s + self.nextWord


def analyze(input_path, output_path, encoding='UTF-8'):

    print('\n\nAnalizuje plik {0}'.format(input_path))

    Entries = []

    with open(file=input_path, encoding=encoding) as data:
        # licznik liniej
        i = 1

        # odczytaj linijke danych
        while True:
            line = data.readline()

            if not line:
                break

            print('Przetwarzam linijke nr: {0:,d}'.format(i).replace(',', ' '), end='\r')
            i += 1

            new_entry = Entry()

            # analizuje linijke danych
            analyzedLine = analyzeLine(line)

            # forma przyimka
            new_entry.prepForm = analyzedLine.prepForm
            # czesc mowy
            new_entry.partOfSpeech = analyzedLine.partOfSpeech
            # nastepny wyraz
            new_entry.nextWord = analyzedLine.word
            # usuwam znaki nieliterowe
            chars = ''.join(chr(c) if chr(c).isupper() or chr(c).islower() else '_' for c in range(256))
            new_entry.nextWord = new_entry.nextWord.translate(chars).replace('_', '')
            # transkrypcja
            new_entry.transcription = transcribe(new_entry.nextWord.lower(), RulesA)
            # pozostawienie poczatkowych spolglosek
            vowels = ['a', 'e', 'i', 'o', 'y', 'u', 'ę', 'ą']
            new_entry.transcription = truncateEnd(new_entry.transcription, vowels, 'V')
            # kategorie spolglosek
            new_entry.categories = transcribe(new_entry.transcription, RulesB)
            # liczba wystapienia
            new_entry.no = analyzedLine.no
            # lemat
            new_entry.lemma = analyzedLine.lemma
            # wokalizacja
            new_entry.voc = analyzedLine.voc
            # przypadek
            new_entry.case = analyzedLine.rec

            Entries.append(new_entry)

    with open(file=output_path, mode="w", encoding=encoding) as _file:
        for entry in Entries:
            _file.write(entry.GetLine() + '\n')
