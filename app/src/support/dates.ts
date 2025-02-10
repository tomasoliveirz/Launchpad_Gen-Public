import { numberToTwoDigit } from "./numbers";

export function formatDate(date:Date)
{
    return date.getFullYear()+"/"+numberToTwoDigit(date.getMonth()+1)+"/"+numberToTwoDigit(date.getDate());
}