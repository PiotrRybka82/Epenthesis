# Typ zawierajacy regule transrypcyjna
class Rule:

    # Konstruktor reguly:
    # rule_name - nazwa reguly
    # left_sets - lista zbiorow po lewej stronie (na poczatku, do zmiany, do odnalezienia)
    # right sets - lista zbiorow po prawej stronie (na koncu, po zmianie, wynik zmiany)
    def __init__(self, rule_name, left_sets, right_sets):
        self.name = rule_name

        for i in range(len(left_sets)):
            # sprawdzenie i-tych zbiorow po lewej i prawej stronie
            checkSets(left_sets[i], right_sets[i])

        # jesli nie zostanie zgloszony blad, konstruktor zapisuje zbiory
        self.left_sets = left_sets
        self.right_sets = right_sets


# Funkcja sprawdzajaca licznosc zbiorow
def checkSets(set1, set2):
    for i in range(len(set1)):
        if (len(set1) != len(set2)):
            n1 = len(set1)
            n2 = len(set2)
            raise IndexError(set1 + ' of ' + n1 + ' elements does not match ' + set2 + ' of ' + n2 + ' elements!')


# Funkcja zwracaąca iloczyny kartezjansie n zbiorow
def product(sets):
    res = sets[0]
    for i in range(1, len(sets)):
        res = semi_product(sets[i], res)
    return res


# Funkcja zwracajaca iloczyn kartezjanski 2 zbiorow
def semi_product(new_set, old_sets):
    res = []
    for i in range(len(old_sets)):
        for j in range(len(new_set)):
            res.append(old_sets[i] + new_set[j])
    return res


# Funkcja zastepujaca wynik iloczynu kartezjanskiego zbiorow
def replace(input_text, left_sets, right_sets):
    # Tworze iloczyny kartezjansie zbiorow lewych i prawych:
    left = product(left_sets)
    right = product(right_sets)

    # Przepisuje lewe zbiory znakow na prawe zbiory znakow
    for i in range(len(left)):
        input_text = input_text.replace(left[i], right[i])

    return input_text


# def replace(input_text, set_1_front, set_1_center, set_1_end, set_2_front, set_2_center, set_2_end):
#     # sprawdzenie, czy licznosc odpowiednich zbiorow jest taka sama
#     c1 = len(set_1_front) == len(set_2_front)
#     c2 = len(set_1_center) == len(set_2_center)
#     c3 = len(set_1_end) == len(set_2_end)
#
#     if (c1 and c2 and c3):
#         # licznosc odpowiednich zbiorow jest taka sama
#         for i in range(len(set_1_front)):
#             for j in range(len(set_1_center)):
#                 for k in range(len(set_1_end)):
#                     t1 = set_1_front[i] + set_1_center[j] + set_1_end[k]
#                     t2 = set_2_front[i] + set_2_center[j] + set_2_end[k]
#                     input_text = input_text.replace(t1, t2)
#
#     return input_text

# Funkcja transkrybujaca tekst wejsciowy
def transcribe(input_text, rules):

    # przegladam wszystkie reguly
    for rule in rules:
        # zamieniam lewe ciagi znakow na prawe ciagi znakow
        input_text = replace(input_text, rule.left_sets, rule.right_sets)

    return input_text


# Funkcja zwracajace poczatek ciagu wejsciowego (text) do pierwszego wystapienia ktoregokolwiek znaku/ciagu znakow z zestawu (stringSet)
# Pierwsze wystapienie znaku/znakow z condSet powoduje odciecie znakow na prawo od tego znaku/znakow wraz z nim/nimi
# i dodanie wskazanego symbolu zastepczego
def truncateEnd(text, stringSet, sign=''):

    # przegladam liste wyjatkow
    for s in stringSet:
        # sprawdzam, czy s znajduje sie w tekscie
        if s in text:
            i = text.find(s)
            text = text[0:i]
    return text + sign
