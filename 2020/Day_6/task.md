[Advent of Code](/)
===================

*   [\[About\]](/2020/about)
*   [\[Events\]](/2020/events)
*   [\[Shop\]](https://teespring.com/stores/advent-of-code)
*   [\[Settings\]](/2020/settings)
*   [\[Log Out\]](/2020/auth/logout)

EAlhovik 18\*

  {:year [2020](/2020)}
=======================

*   [\[Calendar\]](/2020)
*   [\[AoC++\]](/2020/support)
*   [\[Sponsors\]](/2020/sponsors)
*   [\[Leaderboard\]](/2020/leaderboard)
*   [\[Stats\]](/2020/stats)

Our [sponsors](/2020/sponsors) help make Advent of Code possible:

[GitHub](https://github.com/) - We're hiring engineers to make GitHub fast. Interested? Email fast@github.com with details of exceptional performance work you've done in the past.

window.addEventListener('click', function(e,s,r){if(e.target.nodeName==='CODE'&&e.detail===3){s=window.getSelection();s.removeAllRanges();r=document.createRange();r.selectNodeContents(e.target);s.addRange(r);}});

\--- Day 6: Custom Customs ---
------------------------------

As your flight approaches the regional airport where you'll switch to a much larger plane, [customs declaration forms](https://en.wikipedia.org/wiki/Customs_declaration) are distributed to the passengers.

The form asks a series of 26 yes-or-no questions marked `a` through `z`. All you need to do is identify the questions for which _anyone in your group_ answers "yes". Since your group is just you, this doesn't take very long.

However, the person sitting next to you seems to be experiencing a language barrier and asks if you can help. For each of the people in their group, you write down the questions for which they answer "yes", one per line. For example:

    abcx
    abcy
    abcz
    

In this group, there are _`6`_ questions to which anyone answered "yes": `a`, `b`, `c`, `x`, `y`, and `z`. (Duplicate answers to the same question don't count extra; each question counts at most once.)

Another group asks for your help, then another, and eventually you've collected answers from every group on the plane (your puzzle input). Each group's answers are separated by a blank line, and within each group, each person's answers are on a single line. For example:

    abc
    
    a
    b
    c
    
    ab
    ac
    
    a
    a
    a
    a
    
    b
    

This list represents answers from five groups:

*   The first group contains one person who answered "yes" to _`3`_ questions: `a`, `b`, and `c`.
*   The second group contains three people; combined, they answered "yes" to _`3`_ questions: `a`, `b`, and `c`.
*   The third group contains two people; combined, they answered "yes" to _`3`_ questions: `a`, `b`, and `c`.
*   The fourth group contains four people; combined, they answered "yes" to only _`1`_ question, `a`.
*   The last group contains one person who answered "yes" to only _`1`_ question, `b`.

In this example, the sum of these counts is `3 + 3 + 3 + 1 + 1` = _`11`_.

For each group, count the number of questions to which anyone answered "yes". _What is the sum of those counts?_

Your puzzle answer was `6878`.

\--- Part Two ---
-----------------

As you finish the last group's customs declaration, you notice that you misread one word in the instructions:

You don't need to identify the questions to which _anyone_ answered "yes"; you need to identify the questions to which _everyone_ answered "yes"!

Using the same example as above:

    abc
    
    a
    b
    c
    
    ab
    ac
    
    a
    a
    a
    a
    
    b
    

This list represents answers from five groups:

*   In the first group, everyone (all 1 person) answered "yes" to _`3`_ questions: `a`, `b`, and `c`.
*   In the second group, there is _no_ question to which everyone answered "yes".
*   In the third group, everyone answered yes to only _`1`_ question, `a`. Since some people did not answer "yes" to `b` or `c`, they don't count.
*   In the fourth group, everyone answered yes to only _`1`_ question, `a`.
*   In the fifth group, everyone (all 1 person) answered "yes" to _`1`_ question, `b`.

In this example, the sum of these counts is `3 + 0 + 1 + 1 + 1` = _`6`_.

For each group, count the number of questions to which _everyone_ answered "yes". _What is the sum of those counts?_

Your puzzle answer was `3464`.

Both parts of this puzzle are complete! They provide two gold stars: \*\*

At this point, you should [return to your Advent calendar](/2020) and try another puzzle.

If you still want to see it, you can [get your puzzle input](6/input).

You can also \[Shareon [Twitter](https://twitter.com/intent/tweet?text=I%27ve+completed+%22Custom+Customs%22+%2D+Day+6+%2D+Advent+of+Code+2020&url=https%3A%2F%2Fadventofcode%2Ecom%2F2020%2Fday%2F6&related=ericwastl&hashtags=AdventOfCode) [Mastodon](javascript:void(0);)\] this puzzle.

(function(i,s,o,g,r,a,m){i\['GoogleAnalyticsObject'\]=r;i\[r\]=i\[r\]||function(){ (i\[r\].q=i\[r\].q||\[\]).push(arguments)},i\[r\].l=1\*new Date();a=s.createElement(o), m=s.getElementsByTagName(o)\[0\];a.async=1;a.src=g;m.parentNode.insertBefore(a,m) })(window,document,'script','//www.google-analytics.com/analytics.js','ga'); ga('create', 'UA-69522494-1', 'auto'); ga('set', 'anonymizeIp', true); ga('send', 'pageview');