
/**
 * Capitalize first letter
 * @param text the text want to be capitalize
 * @returns
 */
export function capitalize(text: string): string {
  return text.substring(0, 1).toUpperCase() + text.substring(1, text.length).toLowerCase();
}

/**
 * Get the current timestamp
 * @returns
 */
export function currentTimestamp(): number {
  return Math.ceil(new Date().getTime() / 1000);
}

/**
 * Filter the Non null object to make sure the object is valid
 * @param obj filter object
 * @returns
 */
export function filterObject<T extends Record<string, unknown>>(obj: T) {
  return Object.fromEntries(
    Object.entries(obj).filter(([, value]) => value !== undefined && value !== null)
  );
}
