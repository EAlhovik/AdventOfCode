[Advent of Code](/)
===================

*   [\[About\]](/2020/about)
*   [\[Events\]](/2020/events)
*   [\[Shop\]](https://teespring.com/stores/advent-of-code)
*   [\[Settings\]](/2020/settings)
*   [\[Log Out\]](/2020/auth/logout)

EAlhovik 35\*

  {:year [2020](/2020)}
=======================

*   [\[Calendar\]](/2020)
*   [\[AoC++\]](/2020/support)
*   [\[Sponsors\]](/2020/sponsors)
*   [\[Leaderboard\]](/2020/leaderboard)
*   [\[Stats\]](/2020/stats)

Our [sponsors](/2020/sponsors) help make Advent of Code possible:

[American Express](https://jobs.americanexpress.com/technology) - We architect, code and ship software that makes us an essential part of our customers’ digital lives. Work with the latest tech and back the engineering community through open source. Find your place in tech on #TeamAmex.

window.addEventListener('click', function(e,s,r){if(e.target.nodeName==='CODE'&&e.detail===3){s=window.getSelection();s.removeAllRanges();r=document.createRange();r.selectNodeContents(e.target);s.addRange(r);}});

\--- Day 18: Operation Order ---
--------------------------------

As you look out the window and notice a heavily-forested continent slowly appear over the horizon, you are interrupted by the child sitting next to you. They're curious if you could help them with their math homework.

Unfortunately, it seems like this "math" [follows different rules](https://www.youtube.com/watch?v=3QtRK7Y2pPU&t=15) than you remember.

The homework (your puzzle input) consists of a series of expressions that consist of addition (`+`), multiplication (`*`), and parentheses (`(...)`). Just like normal math, parentheses indicate that the expression inside must be evaluated before it can be used by the surrounding expression. Addition still finds the sum of the numbers on both sides of the operator, and multiplication still finds the product.

However, the rules of _operator precedence_ have changed. Rather than evaluating multiplication before addition, the operators have the _same precedence_, and are evaluated left-to-right regardless of the order in which they appear.

For example, the steps to evaluate the expression `1 + 2 * 3 + 4 * 5 + 6` are as follows:

    1 + 2 * 3 + 4 * 5 + 6
      3   * 3 + 4 * 5 + 6
          9   + 4 * 5 + 6
             13   * 5 + 6
                 65   + 6
                     71
    

Parentheses can override this order; for example, here is what happens if parentheses are added to form `1 + (2 * 3) + (4 * (5 + 6))`:

    1 + (2 * 3) + (4 * (5 + 6))
    1 +    6    + (4 * (5 + 6))
         7      + (4 * (5 + 6))
         7      + (4 *   11   )
         7      +     44
                51
    

Here are a few more examples:

*   `2 * 3 + (4 * 5)` becomes _`26`_.
*   `5 + (8 * 3 + 9 + 3 * 4 * 3)` becomes _`437`_.
*   `5 * 9 * (7 * 3 * 3 + 9 * 3 + (8 + 6 * 4))` becomes _`12240`_.
*   `((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2` becomes _`13632`_.

Before you can help with the homework, you need to understand it yourself. _Evaluate the expression on each line of the homework; what is the sum of the resulting values?_

Your puzzle answer was `5019432542701`.

The first half of this puzzle is complete! It provides one gold star: \*

\--- Part Two ---
-----------------

You manage to answer the child's questions and they finish part 1 of their homework, but get stuck when they reach the next section: _advanced_ math.

Now, addition and multiplication have _different_ precedence levels, but they're not the ones you're familiar with. Instead, addition is evaluated _before_ multiplication.

For example, the steps to evaluate the expression `1 + 2 * 3 + 4 * 5 + 6` are now as follows:

    1 + 2 * 3 + 4 * 5 + 6
      3   * 3 + 4 * 5 + 6
      3   *   7   * 5 + 6
      3   *   7   *  11
         21       *  11
             231
    

Here are the other examples from above:

*   `1 + (2 * 3) + (4 * (5 + 6))` still becomes _`51`_.
*   `2 * 3 + (4 * 5)` becomes _`46`_.
*   `5 + (8 * 3 + 9 + 3 * 4 * 3)` becomes _`1445`_.
*   `5 * 9 * (7 * 3 * 3 + 9 * 3 + (8 + 6 * 4))` becomes _`669060`_.
*   `((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2` becomes _`23340`_.

_What do you get if you add up the results of evaluating the homework problems using these new rules?_

Answer:  

Although it hasn't changed, you can still [get your puzzle input](18/input).

You can also \[Shareon [Twitter](https://twitter.com/intent/tweet?text=I%27ve+completed+Part+One+of+%22Operation+Order%22+%2D+Day+18+%2D+Advent+of+Code+2020&url=https%3A%2F%2Fadventofcode%2Ecom%2F2020%2Fday%2F18&related=ericwastl&hashtags=AdventOfCode) [Mastodon](javascript:void(0);)\] this puzzle.

(function(i,s,o,g,r,a,m){i\['GoogleAnalyticsObject'\]=r;i\[r\]=i\[r\]||function(){ (i\[r\].q=i\[r\].q||\[\]).push(arguments)},i\[r\].l=1\*new Date();a=s.createElement(o), m=s.getElementsByTagName(o)\[0\];a.async=1;a.src=g;m.parentNode.insertBefore(a,m) })(window,document,'script','//www.google-analytics.com/analytics.js','ga'); ga('create', 'UA-69522494-1', 'auto'); ga('set', 'anonymizeIp', true); ga('send', 'pageview');