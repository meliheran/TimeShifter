# TimeShifter.cs

![ezgif-5-d0151f6d12](https://user-images.githubusercontent.com/11661323/158804518-39a2ebb9-caf3-4ee4-88d2-a2dd07697758.gif)

## What is Time Shifter ?

Time Shifter with a one sentences "Do not make a database call, wait ! Oh still user is typing some text. Time to go ? No still typing wait more. Hmm seems good now, lets search make a database request and search on db (or api etc.) !"
Time Shifter is a solution to postpone serverside requests/calls for a specific time and making it only on idle time. It is useful for SearchBoxes to make a db call when user is idle. Whenever user continue typing etc. db call or api request pospones for a specific time.

## Problem

* Calling DB/Api on every event or on action and can't cancelling previous one
* Recursive database requests or api requests
* Unexpectable user behaviour

## Benefits

Useful for search text areas.
Prevents several DB or Api calls in every key press or x action.

## Purpose of Time Shifter ?

Solves running calls depending on unexpected situations. If you don't know when to make a DB call depending on user key typings TimeShifter will solve that issue. It is not a good way to call db or api on every user typing or call it every x seconds. The better way is "When user stops typing" so timeshifter calls only 1 time when user is idle.
