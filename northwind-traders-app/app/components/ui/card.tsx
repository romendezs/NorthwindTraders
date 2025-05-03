export function Card({ children }: { children: React.ReactNode }) {
    return <div className="bg-white shadow-md rounded-2xl p-4">{children}</div>;
}

export function CardContent({ children, className = "" }: { children: React.ReactNode; className?: string }) {
    return <div className={`p-4 ${className}`}>{children}</div>;
}