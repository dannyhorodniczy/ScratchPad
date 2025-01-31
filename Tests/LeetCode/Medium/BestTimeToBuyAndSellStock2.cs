using FluentAssertions;
using Xunit;

namespace Tests.LeetCode.Medium;

// https://leetcode.com/problems/best-time-to-buy-and-sell-stock-ii
public class BestTimeToBuyAndSellStock2
{
    [Theory]
    [InlineData(new int[] { 7, 1, 5, 3, 6, 4 }, 7)]
    [InlineData(new int[] { 1, 2, 3, 4, 5 }, 4)]
    [InlineData(new int[] { 7, 6, 4, 3, 1 }, 0)]
    [InlineData(new int[] { 6, 1, 3, 2, 4, 7 }, 7)]
    public void Given_WhenMaxProfit_Then(int[] nums, int profit)
    {
        int result = MaxProfit(nums);
        result.Should().Be(profit);
    }

    private static int MaxProfit(int[] prices)
    {
        int maxProfit = 0;
        int tempMaxProfit = 0;

        int i = 0;
        int j = 1;
        while (j < prices.Length)
        {
            int possibleMaxProfit = prices[j] - prices[i];
            if (possibleMaxProfit > tempMaxProfit)
            {
                tempMaxProfit = possibleMaxProfit;
            }
            else
            {
                maxProfit += tempMaxProfit;
                tempMaxProfit = 0;
                i = j;
            }

            j++;
        }

        return maxProfit + tempMaxProfit;
    }

    [Theory]
    [InlineData(new int[] { 7, 1, 5, 3, 6, 4 }, 7)]
    [InlineData(new int[] { 1, 2, 3, 4, 5 }, 4)]
    [InlineData(new int[] { 7, 6, 4, 3, 1 }, 0)]
    [InlineData(new int[] { 6, 1, 3, 2, 4, 7 }, 7)]
    public void Given_WhenMaxProfit2_Then(int[] nums, int profit)
    {
        int result = MaxProfit2(nums);
        result.Should().Be(profit);
    }

    public int MaxProfit2(int[] prices)
    {
        /*
         * I remember that my initial solution was not a filter
         * it was more of a successive approximation
         * but I believe the correct solution is more of a filter
         * 
         * let's try:
         * 1. compare the price of the current day and the next day
         * 2. if the next day is lower, then move to the next day and repeat the comparison
         * 3. if the next day is higher, then profit is possible
         * - temporarily save the amount of profit, set a flag, continue to check successive days until the price drops (rather the possible profit is lower)
         * - once the price drops, permenantly save the profit
         * 
         */
        int totalProfit = 0;
        int possibleProfit = 0;
        int temporaryLow = prices[0];

        for (int i = 1; i < prices.Length; i++)
        {
            int priceDifference = prices[i] - temporaryLow;
            if (priceDifference > 0 && priceDifference > possibleProfit)
            {
                possibleProfit = priceDifference;
            }
            else
            {
                totalProfit += possibleProfit;
                possibleProfit = 0;
                temporaryLow = prices[i];
            }

        }

        if (possibleProfit > 0)
        {
            totalProfit += possibleProfit;
        }

        return totalProfit;

    }
}
