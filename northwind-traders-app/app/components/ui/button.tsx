export function Button({ variant = "default", ...props }: React.ButtonHTMLAttributes<HTMLButtonElement> & { variant?: "default" | "outline" | "destructive" | "ghost" }) {
    const base = "px-4 py-2 rounded font-semibold";
    const variants: Record<string, string> = {
      default: "bg-blue-600 text-white hover:bg-blue-700",
      outline: "border border-gray-400 text-gray-700 hover:bg-gray-100",
      destructive: "bg-red-600 text-white hover:bg-red-700",
      ghost: "text-gray-600 hover:text-black",
    };
    return <button className={`${base} ${variants[variant]}`} {...props} />;
}