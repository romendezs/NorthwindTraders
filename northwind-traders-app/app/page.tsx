import { Input } from "./components/ui/input";
import { Button } from "./components/ui/button";
import { Card, CardContent } from "./components/ui/card";
//import { MapPin } from "./lucide-react";

export default function Home() {
  return (
    <div className="p-6 space-y-4">
      {/* Header buttons */}
      <div className="flex justify-between">
        <div className="space-x-2">
          <Button>New</Button>
          <Button variant="outline">Save</Button>
          <Button variant="destructive">Delete</Button>
        </div>
        <div className="space-x-2">
          <Button>Generate</Button>
          <Button variant="ghost">◀</Button>
          <Button variant="ghost">▶</Button>
          <Button variant="ghost">🔍</Button>
        </div>
      </div>

      {/* Customer & Order Info */}
      <div className="grid grid-cols-2 gap-4">
        <Input placeholder="Customer" />
        <Input placeholder="Shipping address" />
        <Input placeholder="Order date" />
        <Input placeholder="Employee" />
      </div>

      {/* Lines Section */}
      <Card>
        <CardContent className="p-4 space-y-2">
          <div className="flex justify-between">
            <h2 className="text-lg font-semibold">Lines</h2>
            <div className="space-x-2">
              <Button>New</Button>
              <Button variant="outline">Save</Button>
              <Button variant="destructive">Delete</Button>
            </div>
          </div>
          <div className="grid grid-cols-4 gap-2 font-semibold">
            <span>Product</span>
            <span>Qty</span>
            <span>Unit P.</span>
            <span>Total</span>
          </div>
          {[1, 2, 3].map((_, i) => (
            <div key={i} className="grid grid-cols-4 gap-2">
              <Input />
              <Input />
              <Input />
              <Input />
            </div>
          ))}
        </CardContent>
      </Card>

      {/* Validated Address */}
      <Card>
        <CardContent className="p-4 space-y-2">
          <h2 className="text-lg font-semibold">Validated address</h2>
          <div className="grid grid-cols-3 gap-4">
            <Input placeholder="Street" />
            <Input placeholder="City" />
            <Input placeholder="State" />
            <Input placeholder="Postal code" />
            <Input placeholder="Country" />
            <Input placeholder="Geocoded coordinates" />
          </div>
        </CardContent>
      </Card>

      {/* Embedded Map */}
      <div className="border rounded-xl overflow-hidden">
        <iframe
          title="Map"
          width="100%"
          height="300"
          frameBorder="0"
          src="https://www.google.com/maps/embed/v1/place?q=RSM+US+LLP,+Chicago,+IL&key=AIzaSyBSgEjwx_ohzDlzvEO36qiCAzLaUFHBB1A"
          allowFullScreen
        />
      </div>
    </div>
  );
}
