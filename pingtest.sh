#!/bin/bash
num=0
counter=$1
while [[ $num -lt $counter ]]
do 
    http_response=$(curl -k -s -w ',%{http_code},%{time_total}s ' localhost)
    echo $http_response | awk -F',' '{print $2 FS $3 FS $1}'
    num=$(($num+1))
done