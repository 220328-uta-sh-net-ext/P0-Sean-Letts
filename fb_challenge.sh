read sampleInput
f0=0
f1=1
current=1;
count=1;
if [ $sampleInput -gt 40 ]
then
    echo "Error, incorrect input entered."
    exit

elif [ $sampleInput -lt 0 ]
then
    echo "Error, incorrect input entered."
    exit
elif [ $sampleInput -eq 0 ]
then
    echo "0"
    exit
elif [ $sampleInput -eq 1 ]
then
    echo "1"
    exit
else
#where it ought to go
#needs to loop
    while [ $sampleInput -ne $count ]
    do
        current=$(( $f1 + $f0 ))
        f0=$f1
        f1=$current
        count=$(( $count + 1))
    done
fi
#logic for splitting first and second digit
#echo "$f1"
#ones=$(( $f1 % 10 ))
#echo "$ones"
#tens=$(( $f1 / 10))
#echo "$tens"
#results=$(( $ones + $tens ))
#echo $results

while [ $current -ge 10 ]
do
    val=$(( $current % 10 ))
    current=$(( $current / 10 ))
    results=$(( $results + $val ))
done
results=$(( $results + $current ))
echo $results