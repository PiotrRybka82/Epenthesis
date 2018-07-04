from collections import namedtuple


def analyzeLine(line):
    # Forma pierwszego słowa
    word1 = line.split(' ')[0]

    # Forma drugiego słowa
    word2 = line.split(';')[1]

    # Liczba wystąpień
    try:
        no = int(line.split(';')[2])
    except Exception as e:
        no = 0

    # Wartosci kategorii fleksyjnych i lemat
    i1 = str(line).index('[') + 1
    i2 = str(line).index(']')

    temp = line[i1:i2].split(':')

    # Lemat
    lemma = temp[0]

    # Czesc mowy
    partOfSpeech = temp[1]

    # Rekcja przyimka i wokalicznosc
    rec = ''
    voc = ''
    if partOfSpeech == 'prep':
        rec = temp[2]
        voc = temp[3]

    # Tworze namedtuple
    result = namedtuple('line', 'word, prepForm lemma, partOfSpeech, rec, voc, no')

    # result = namedtuple('line', ['pair', 'lemma', 'partOfSpeech', 'rec', 'voc'])
    # result = namedtuple('line', ['pair', 'pairSet'])
    #
    # pair = Pair(word1, word2, no)
    # pairset = PairSet(lemma, partOfSpeech, rec, voc)
    #
    # return result(pair, pairset)
    return result(word2, word1, lemma, partOfSpeech, rec, voc, no)
