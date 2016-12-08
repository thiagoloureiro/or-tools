************************************************************************
file with basedata            : md74_.bas
initial value random generator: 27167
************************************************************************
projects                      :  1
jobs (incl. supersource/sink ):  14
horizon                       :  99
RESOURCES
  - renewable                 :  2   R
  - nonrenewable              :  2   N
  - doubly constrained        :  0   D
************************************************************************
PROJECT INFORMATION:
pronr.  #jobs rel.date duedate tardcost  MPM-Time
    1     12      0       18        6       18
************************************************************************
PRECEDENCE RELATIONS:
jobnr.    #modes  #successors   successors
   1        1          3           2   3   4
   2        3          3           5   7  12
   3        3          3           5   6   7
   4        3          3           6   8  10
   5        3          2          10  13
   6        3          2           9  12
   7        3          2          10  13
   8        3          2           9  12
   9        3          2          11  13
  10        3          1          11
  11        3          1          14
  12        3          1          14
  13        3          1          14
  14        1          0        
************************************************************************
REQUESTS/DURATIONS:
jobnr. mode duration  R 1  R 2  N 1  N 2
------------------------------------------------------------------------
  1      1     0       0    0    0    0
  2      1     5       6    0    0    7
         2     8       5    0    0    3
         3     9       3    0    5    0
  3      1     3       5    0    2    0
         2     9       3    0    0    3
         3    10       2    0    0    3
  4      1     2       0    9    0    6
         2     2       3    0    9    0
         3     4       0    7    8    0
  5      1     6       0    6    0    6
         2     9       8    0    8    0
         3    10       5    0    0    5
  6      1     1       0    4    0    6
         2     2       8    0    0    6
         3     9       8    0    4    0
  7      1     1       0    8    0    6
         2     3       0    4    5    0
         3     5       9    0    4    0
  8      1     1       0    8    0    9
         2     5       4    0    0    9
         3     7       0    7    6    0
  9      1     2       5    0    0    5
         2     5       3    0    4    0
         3     9       0    2    4    0
 10      1     2       0    6    0   10
         2     4       6    0    0    9
         3    10       5    0    0    8
 11      1     5       7    0    0    7
         2     7       0    8    1    0
         3     9       5    0    0    7
 12      1     8       0    7    0    6
         2     9       7    0    0    5
         3    10       6    0    9    0
 13      1     4       0    7    0    7
         2     6       5    0    0    5
         3     7       0    7    3    0
 14      1     0       0    0    0    0
************************************************************************
RESOURCEAVAILABILITIES:
  R 1  R 2  N 1  N 2
   16   12   28   43
************************************************************************