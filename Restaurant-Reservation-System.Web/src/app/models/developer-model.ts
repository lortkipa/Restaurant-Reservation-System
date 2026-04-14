import { PersonModel } from "./user-model";

export interface TeamMember {
  id: number;
  person: PersonModel;   // 👈 make optional
  role: string;
  githubLink: string | null;
  linkedinLink: string | null;
  portfolioLink: string | null;
}