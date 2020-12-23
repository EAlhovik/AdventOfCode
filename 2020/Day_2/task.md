[Advent of Code](/)
===================

*   [\[About\]](/2020/about)
*   [\[Events\]](/2020/events)
*   [\[Shop\]](https://teespring.com/stores/advent-of-code)
*   [\[Settings\]](/2020/settings)
*   [\[Log Out\]](/2020/auth/logout)

EAlhovik 18\*

   var y=[2020](/2020);
=======================

*   [\[Calendar\]](/2020)
*   [\[AoC++\]](/2020/support)
*   [\[Sponsors\]](/2020/sponsors)
*   [\[Leaderboard\]](/2020/leaderboard)
*   [\[Stats\]](/2020/stats)

Our [sponsors](/2020/sponsors) help make Advent of Code possible:

[Educative.io](https://www.educative.io/adventofcode) - From CSS to System Design, gain in-demand tech skills at the speed you want. Text-based courses with live coding environments help you learn without the fluff

window.addEventListener('click', function(e,s,r){if(e.target.nodeName==='CODE'&&e.detail===3){s=window.getSelection();s.removeAllRanges();r=document.createRange();r.selectNodeContents(e.target);s.addRange(r);}});

\--- Day 2: Password Philosophy ---
-----------------------------------

Your flight departs in a few days from the coastal airport; the easiest way down to the coast from here is via [toboggan](https://en.wikipedia.org/wiki/Toboggan).

The shopkeeper at the North Pole Toboggan Rental Shop is having a bad day. "Something's wrong with our computers; we can't log in!" You ask if you can take a look.

Their password database seems to be a little corrupted: some of the passwords wouldn't have been allowed by the Official Toboggan Corporate Policy that was in effect when they were chosen.

To try to debug the problem, they have created a list (your puzzle input) of _passwords_ (according to the corrupted database) and _the corporate policy when that password was set_.

For example, suppose you have the following list:

    1-3 a: abcde
    1-3 b: cdefg
    2-9 c: ccccccccc
    

Each line gives the password policy and then the password. The password policy indicates the lowest and highest number of times a given letter must appear for the password to be valid. For example, `1-3 a` means that the password must contain `a` at least `1` time and at most `3` times.

In the above example, `_2_` passwords are valid. The middle password, `cdefg`, is not; it contains no instances of `b`, but needs at least `1`. The first and third passwords are valid: they contain one `a` or nine `c`, both within the limits of their respective policies.

_How many passwords are valid_ according to their policies?


\--- Part Two ---
-----------------

While it appears you validated the passwords correctly, they don't seem to be what the Official Toboggan Corporate Authentication System is expecting.

The shopkeeper suddenly realizes that he just accidentally explained the password policy rules from his old job at the sled rental place down the street! The Official Toboggan Corporate Policy actually works a little differently.

Each policy actually describes two _positions in the password_, where `1` means the first character, `2` means the second character, and so on. (Be careful; Toboggan Corporate Policies have no concept of "index zero"!) _Exactly one of these positions_ must contain the given letter. Other occurrences of the letter are irrelevant for the purposes of policy enforcement.

Given the same example list from above:

*   `1-3 a: _a_b_c_de` is _valid_: position `1` contains `a` and position `3` does not.
*   `1-3 b: _c_d_e_fg` is _invalid_: neither position `1` nor position `3` contains `b`.
*   `2-9 c: c_c_cccccc_c_` is _invalid_: both position `2` and position `9` contain `c`.

_How many passwords are valid_ according to the new interpretation of the policies?
