from phonetics import Rule

RulesA = []

c1 = ['s', 'dz', 'z', 'c', 'n']
c2 = ["ś", "Ź", "ź", "ć", "ń"]
i1 = ['i']
i2 = ['']
v = ["a", "e", "i", "o", "y", "u", "ę", "ą"]
a = [c1, i1, v]
b = [c2, i2, v]
RulesA.append(Rule('Miekkie przed samogloskami innymi niz /i/', a, b))

c1 = ['si', 'dzi', 'ci', 'zi', 'ni']
c2 = ["śi", "Źi", "źi", "ći", "ńi"]
a = [c1]
b = [c2]
RulesA.append(Rule('Miekkie przed /i/', a, b))


c1 = ["p", "b", "m", "f", "w", "t", "d", "r", "l", "k", "g", "ch", "h"]
i2 = ["j"]
a = [c1, i1, v]
b = [c1, i2, v]
RulesA.append(Rule('Zmiekczone przed samogloska', a, b))


w1 = ['w']
c = ["t", "k", "p", "s", "ś", "ć", "c", "S", "C"]
w2 = ['f']
a = [c, w1]
b = [c, w2]
RulesA.append(Rule('Ubezdzwiecznienie [w] po bezdzwiecznych', a, b))

s1 = ["ó", "sz", "rz", "cz", "ch", "dż", "dz"]
s2 = ['u', "S", "R", "C", "x", "Ż", "Z"]
a = [s1]
b = [s2]
RulesA.append(Rule('Dwuznaki i polskie znaki', a, b))


RulesB = []

# Zmiekczone
z1 = ["'"]
z2 = ['']
a = [z1]
b = [z2]
RulesB.append(Rule('Zmiekczone', a, b))

# Nosowe
n1 = ['m', 'n', 'ń']
n2 = ['N', 'N', 'N']
a = [n1]
b = [n2]
RulesB.append(Rule('Nosowe', a, b))

# Zwarte i afrykaty
t1 = ['p', 'b', 't', 'd', 'k', 'g']
t2 = ['T', 'T', 'T', 'T', 'T', 'T']
a = [t1]
b = [t2]
RulesB.append(Rule('Zwarte', a, b))

c1 = ['c', 'C', 'ć', 'Z', 'Ż', 'Ź']
c2 = ['C', 'C', 'C', 'C', 'C', 'C']
a = [c1]
b = [c2]
RulesB.append(Rule('Afrykaty', a, b))

# Trące
s1 = ['f', 'w', 's', 'z', 'S', 'ż', 'ś', 'ź', 'x', 'h']
s2 = ['S', 'S', 'S', 'S', 'S', 'S', 'S', 'S', 'S', 'S']
a = [s1]
b = [s2]
RulesB.append(Rule('Trace', a, b))

# Półotwarte
l1 = ['j', 'ł', 'r', 'l']
l2 = ['L', 'L', 'L', 'L']
a = [l1]
b = [l2]
RulesB.append(Rule('Polotwarte', a, b))
