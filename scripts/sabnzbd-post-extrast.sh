#!/bin/bash
#
# Sample script to use with sabnzbd to handle automatic conversion of xbox360 games into GOD
# This script aim to be more generic (path as vars, etc ...)
#
cd "$1"
echo "> Processing game $3"

if [ ! -d "/media/storage/X360/$3" ]; then
    echo -n "> Creating folder X360/$3 ..."
    mkdir "/media/storage/X360/$3"
    echo " Done"
fi

for i in $(find -name \*.iso); do

    echo ">> Processing ISO $i"

    FOLDER=$(basename $i)

    if [ -d extracted ]; then
        rm -r extracted
    fi

    echo -n ">>> Extracting GOD ..."
    mono /opt/iso2god/bin/iso2god.exe $i extracted/
    #mkdir extracted && touch extracted/default.xex
    echo " Done"

    if [ -d extracted ]; then
        echo ">> Moving GOD to /media/storage/X360/$3/$FOLDER/"
        mv extracted/* "/media/storage/X360/$3/$FOLDER/"
    else
        echo ">> ERROR : extracted folder not found"
        exit 1
    fi

    cd "$1"
done

