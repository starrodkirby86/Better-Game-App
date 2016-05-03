--
-- dichotomy is
-- n, nw, w, sw, s, se, e, ne
-- a  b   c  d   e  f   g  h
--
-- if (a == 0) || (c == 0)
-- then b == 0
--
-- if (a == 0) || (g == 0)
-- then h == 0
--
-- if (e == 0) || (c == 0)
-- then d == 0
--
-- if (e == 0) || (g == 0)
-- then f == 0
--

-- Figures out all the possible cases there are.
allCases = [ listToBinary (a:b:c:d:e:f:g:[h]) | a <- [0,1], b <- [0,1], c <- [0,1], d <- [0,1],
                                                e <- [0,1], f <- [0,1], g <- [0,1], h <- [0,1],
                                                ( (a == 0) || (c == 0) ) <= (b == 0),
                                                ( (a == 0) || (g == 0) ) <= (h == 0),
                                                ( (e == 0) || (c == 0) ) <= (d == 0),
                                                ( (e == 0) || (g == 0) ) <= (f == 0) ]

-- Converts a list of 1,0s to a binary representation (but base-10 showing)
listToBinary :: [Int] -> Int
listToBinary l = let rl = reverse l
                 in convertMe rl 1 where
                   convertMe [] _ = 0
                   convertMe (l:ls) a = l*1*a + convertMe ls (a*10)

-- c'est de la resistance
missingCases = [ a | a <- allCases, notElem a tileDictionary ]

-- Existing cases
tileDictionary = [1110,111110,111000,1000,11111011,11101111,10111110,11111010,10101000,11101000,10001111,11111111,11111000,10001000,11111110,10111111,10101111,11101011,10001010,10001011,10000011,11100011,11100000,10000000,1010,101000,10101110,10111010,10101010,10,100010,100000,0,10000010,10100000,10101011,11101010]
