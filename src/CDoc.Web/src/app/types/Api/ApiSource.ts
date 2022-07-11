export interface ApiSource {
    repository: string;
    cache: string;
    auto_update_seconds: number;
    pull_before_read: boolean;
}