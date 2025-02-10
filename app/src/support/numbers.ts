export function numberToTwoDigit(number:number)
{
    return number.toLocaleString('en-US', {
        minimumIntegerDigits: 2,
        useGrouping: false
      })
}