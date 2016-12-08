************************************************************************
file with basedata            : cr447_.bas
initial value random generator: 1070042647
************************************************************************
projects                      :  1
jobs (incl. supersource/sink ):  18
horizon                       :  127
RESOURCES
  - renewable                 :  4   R
  - nonrenewable              :  2   N
  - doubly constrained        :  0   D
************************************************************************
PROJECT INFORMATION:
pronr.  #jobs rel.date duedate tardcost  MPM-Time
    1     16      0       23        0       23
************************************************************************
PRECEDENCE RELATIONS:
jobnr.    #modes  #successors   successors
   1        1          3           2   3   4
   2        3          2           5  15
   3        3          3           6   7  17
   4        3          3          12  13  14
   5        3          3           9  10  13
   6        3          3           8  10  14
   7        3          2           8  14
   8        3          2          11  15
   9        3          2          11  17
  10        3          1          11
  11        3          1          12
  12        3          1          16
  13        3          2          16  17
  14        3          2          15  16
  15        3          1          18
  16        3          1          18
  17        3          1          18
  18        1          0        
************************************************************************
REQUESTS/DURATIONS:
jobnr. mode duration  R 1  R 2  R 3  R 4  N 1  N 2
------------------------------------------------------------------------
  1      1     0       0    0    0    0    0    0
  2      1     2       9    2    6    9    6    8
         2     6       9    1    4    7    3    8
         3     6       9    1    3    7    4    7
  3      1     6       9   10    8    6   10    7
         2     7       7    5    2    3    8    6
         3     7       6    1    5    3    8    3
  4      1     5       6   10    5    7    3    7
         2     6       4   10    5    5    3    6
         3    10       3   10    4    3    2    6
  5      1     1       8   10    8   10    4    5
         2     2       7   10    6    5    4    4
         3     8       4    9    5    3    3    2
  6      1     4      10    8   10    5    3    9
         2     7       8    5    5    4    2    9
         3    10       5    3    4    4    2    8
  7      1     4       4    6    5    3    6    3
         2     8       4    6    5    2    6    2
         3    10       3    5    4    2    5    2
  8      1     1       5    7    8    4    6    6
         2     9       4    6    7    4    5    5
         3    10       1    5    6    4    5    3
  9      1     2       7    4   10    4    4    5
         2     7       4    3    9    4    3    5
         3     8       2    2    9    3    3    5
 10      1     1       4    9   10    5    6    2
         2     1       5    8   10    4    6    2
         3     3       3    8    9    4    5    1
 11      1     3       6    6    8    6    6    4
         2     6       6    6    6    5    5    3
         3     8       5    6    1    4    5    3
 12      1     7       9    8    9    2    5    3
         2     7       8    9    6    1    5    9
         3     7       7    8    9    1    5    6
 13      1     1       7    5   10    8    6    8
         2     4       6    5    9    8    6    7
         3     5       5    3    8    8    6    6
 14      1     4       9    8    7    8    7   10
         2     5       7    8    5    7    6    9
         3    10       4    7    5    7    3    9
 15      1     4       5    7   10    8    2    9
         2     6       4    6   10    8    1    7
         3     8       2    5   10    7    1    6
 16      1     2       7    9    5    7    6    9
         2     6       5    7    5    7    5    9
         3     9       3    6    4    7    5    8
 17      1     1       4    4   10    5    8    3
         2     7       4    2    5    5    8    2
         3     8       4    2    5    5    7    1
 18      1     0       0    0    0    0    0    0
************************************************************************
RESOURCEAVAILABILITIES:
  R 1  R 2  R 3  R 4  N 1  N 2
   20   25   21   19   78   89
************************************************************************